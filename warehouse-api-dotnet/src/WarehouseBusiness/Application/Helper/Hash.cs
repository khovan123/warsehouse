using Application.Helper.Options;
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
        public static string GenerateJWT(JwtOptions options, string subjectUserId, string? username = null, IEnumerable<string>? roles = null)
        {
            var keyBytes = Encoding.UTF8.GetBytes(options.SecretKey);
            var secretKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Sub, subjectUserId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

            if (!string.IsNullOrWhiteSpace(username))
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, username));

            if (roles is not null)
            {
                foreach (var r in roles.Where(x => !string.IsNullOrWhiteSpace(x)))
                    claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var expires = DateTime.UtcNow.AddMinutes(options.TokenExpiresMinutes);

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
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
