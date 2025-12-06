using Application.DTOs;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository _userRepository, IConfiguration _config)
        {
            this._userRepository = _userRepository;
            this._config = _config;
        }
        public async Task<ApiResponse<Login.Response>> LoginAsync(Login.Request requestPayload, CancellationToken ct)
        {
            var user = await _userRepository.GetByUsername(requestPayload.Username, ct);
            if (user is null)
            {
                return new ApiResponse<Login.Response>.FailedBuilder(
                    "Invalid username",
                    "AUTH_INVALID",
                    StatusCodes.Status401Unauthorized
                    );
            }

            if (! BCrypt.Net.BCrypt.Verify(requestPayload.Password, user.Password))
            {
                return new ApiResponse<Login.Response>.FailedBuilder(
                    "Invalid password",
                    "AUTH_INVALID",
                    StatusCodes.Status401Unauthorized
                    );
            }
            var token = GenerateJWT(user);
            var data = new Login.Response(
                new Login.UserDTO(user.Id, user.Username, user.Email),
                token
            );
            return new ApiResponse<Login.Response>.SuccessBuilder(data, "Login successful");
        }

        private string GenerateJWT(User user) {
            var jwtSection = _config.GetSection("JWT");
            var secretKey_bytes = Encoding.UTF8.GetBytes(jwtSection["SECRET_KEY"]!);
            var secretKey = new SymmetricSecurityKey(secretKey_bytes);

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var expires = DateTime.UtcNow.AddDays(int.Parse(jwtSection["TOKEN_EXPIRES"]!));

            var jwt = new JwtSecurityToken(
                issuer: jwtSection["ISSUER"],
                audience: jwtSection["AUDIENCE"],
                claims,
                expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
