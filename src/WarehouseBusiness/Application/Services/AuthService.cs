using Application.Dtos;
using Application.DTOs;
using Application.Helper;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository _userRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration _config)
        {
            this._userRepository = _userRepository;
            this._refreshTokenRepository = refreshTokenRepository;  
            this._config = _config;
        }
        public async Task<ApiResponse<LoginDTO.ResponseWithRefreshToken>> LoginAsync(LoginDTO.Request requestPayload, CancellationToken ct)
        {
            var user = await _userRepository.GetByUsername(requestPayload.Username, ct);
            if (user is null)
            {
                return new ApiResponse<LoginDTO.ResponseWithRefreshToken>.FailedBuilder(
                    "Invalid username",
                    ApiErrorCode.AuthenticationFailed,
                    StatusCodes.Status401Unauthorized
                    );
            }

            if (! BCrypt.Net.BCrypt.Verify(requestPayload.Password, user.Password))
            {
                return new ApiResponse<LoginDTO.ResponseWithRefreshToken>.FailedBuilder(
                    "Invalid password",
                    ApiErrorCode.AuthenticationFailed,
                    StatusCodes.Status401Unauthorized
                    );
            }
            var token = Hash.GenerateJWT(_config);
            var rawRefreshToken = Hash.GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                UserId = user.Id!,
                TokenHash = Hash.Sha256(rawRefreshToken),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
            };

            _refreshTokenRepository.CreateOne(refreshToken, ct);

            var data = new LoginDTO.ResponseWithRefreshToken(
                new LoginDTO.UserDTO(user.Id, user.Username, user.Email),
                token,
                rawRefreshToken
            );


            return new ApiResponse<LoginDTO.ResponseWithRefreshToken>.SuccessBuilder(data, "Login successful");
        }

        public async Task<ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>> RefreshAccessToken(string token_hash, CancellationToken ct)
        {
            var refreshtoken = await _refreshTokenRepository.GetByTokenHash(token_hash, ct);
            if (refreshtoken is null || refreshtoken.ExpiresAt <= DateTime.UtcNow)
            {
                return new ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>.FailedBuilder("Unauthorized", ApiErrorCode.Unauthorized, StatusCodes.Status401Unauthorized);
            }

            var newRaw = Hash.GenerateRefreshToken();
            var newHash = Hash.Sha256(newRaw);
                
            refreshtoken.RevokedAt = DateTime.UtcNow;
            refreshtoken.ReplacedByTokenHash = newHash;

            _refreshTokenRepository.UpdateOne(refreshtoken, ct);


            var newTokenEntity = new RefreshToken
            {
                UserId = refreshtoken.UserId,
                TokenHash = newHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
            };
            
            _refreshTokenRepository.CreateOne(newTokenEntity, ct);

            var newJwt = Hash.GenerateJWT(_config);

            var data = new RefreshTokenDTO.ResponseWithRefreshToken
            {
                Response = new RefreshTokenDTO.Response { AccessToken = newJwt},
                RefreshToken = newRaw
            };

            return new ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>.SuccessBuilder(data, StatusCode: StatusCodes.Status201Created);
        }
    }
}
