using Application.Dtos;
using Application.Exceptions;
using Application.Helper;
using Application.Helper.Options;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtOptions _jwt;

        public AuthService(IUserRepository _userRepository, IRefreshTokenRepository _refreshTokenRepository, IOptions<JwtOptions> jwtOptions, IValidationRunner validation) : base(validation)
        {
            this._userRepository = _userRepository;
            this._refreshTokenRepository = _refreshTokenRepository;
            _jwt = jwtOptions.Value;
        }
        public async Task<ApiResponse<LoginDTO.ResponseWithRefreshToken>?> LoginAsync(LoginDTO.Request requestPayload, CancellationToken ct)
        {
            await DoCheckValidationAsync(requestPayload, ct);

            var user = await _userRepository.GetByUsernameAsync(requestPayload.Username, ct);
            if (user is null)
            {
                throw new UnauthorizedException("Invalid username");
            }


            if (!BCrypt.Net.BCrypt.Verify(requestPayload.Password, user.Password))
            {
                throw new UnauthorizedException("Invalid password");
            }
            var token = Hash.GenerateJWT(_jwt, user.Id!, user.Username);
            var rawRefreshToken = Hash.GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                UserId = user.Id!,
                TokenHash = Hash.Sha256(rawRefreshToken),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
            };

            await _refreshTokenRepository.CreateOneAsync(refreshToken, ct);

            var data = new LoginDTO.ResponseWithRefreshToken(
                new LoginDTO.UserDTO(user.Id, user.Username, user.Email),
                token,
                rawRefreshToken
            );


            return new ApiResponse<LoginDTO.ResponseWithRefreshToken>(data, "Login successfully!");
        }

        public async Task<ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>?> RefreshAccessTokenAsync(string token_hash, CancellationToken ct)
        {
            var refreshtoken = await _refreshTokenRepository.GetByTokenHashAsync(token_hash, ct) ?? throw new InvalidRefreshTokenException();
            if (refreshtoken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new InvalidRefreshTokenException();
            }

            var newRaw = Hash.GenerateRefreshToken();
            var newHash = Hash.Sha256(newRaw);

            refreshtoken.RevokedAt = DateTime.UtcNow;
            refreshtoken.ReplacedByTokenHash = newHash;

            await _refreshTokenRepository.UpdateOneAsync(refreshtoken, ct);


            var newTokenEntity = new RefreshToken
            {
                UserId = refreshtoken.UserId,
                TokenHash = newHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
            };

            await _refreshTokenRepository.CreateOneAsync(newTokenEntity, ct);

            var newJwt = Hash.GenerateJWT(_jwt, refreshtoken.UserId);

            var data = new RefreshTokenDTO.ResponseWithRefreshToken
            {
                Response = new RefreshTokenDTO.Response { AccessToken = newJwt },
                RefreshToken = newRaw
            };

            return new ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>(data, statusCode: StatusCodes.Status201Created);
        }
    }
}
