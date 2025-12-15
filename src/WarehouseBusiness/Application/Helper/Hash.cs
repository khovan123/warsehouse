using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Helper
{
    public static class Hash
    {
        public static string GenerateJWT(IConfiguration _config)
        {
            var jwtSection = _config.GetSection("JWT");
            var secretKey_bytes = Encoding.UTF8.GetBytes(jwtSection["SECRET_KEY"]!);
            var secretKey = new SymmetricSecurityKey(secretKey_bytes);

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[] {
           
            //};

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSection["TOKEN_EXPIRES"]!));

            var jwt = new JwtSecurityToken(
                issuer: jwtSection["ISSUER"],
                audience: jwtSection["AUDIENCE"],
                //claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public static string Sha256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
