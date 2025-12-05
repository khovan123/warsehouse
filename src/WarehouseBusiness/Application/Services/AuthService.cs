using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository _userRepository, IConfiguration _config)
        {
            this._userRepository = _userRepository;
            this._config = _config;
        }
        public async Task<Login.Response?> LoginRequest(Login.Request requestPayload, CancellationToken ct)
        {
            var user = await _userRepository.GetByUsername(requestPayload.Username, ct);
            if (user is null)
            {
                return null;
            }

            if(! BCrypt.Net.BCrypt.Verify(requestPayload.Password, user.Password))
            {
                return null;
            }
            var token = GenerateJWT(user);

            return new Login.Response(new Login.UserDTO(user.Id, user.Username, user.Email), token);
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
