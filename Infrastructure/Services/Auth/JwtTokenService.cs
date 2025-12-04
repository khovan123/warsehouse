using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure;

public class JwtTokenService : IJwtTokensService
{
    private readonly JwtOptions _jwt;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _jwt = options.Value;
    }

    public string GetBaseUrl()
    {
        return _jwt.Issuer;
    }

    public SignInResponse GenerateJwtToken(UserCredentials user, CancellationToken ct = default)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = CreateJwtToken(claims, _jwt.AccessTokenLifetime);

        return new SignInResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = _jwt.AccessTokenLifetime
        };
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, int lifetimeInSeconds)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(lifetimeInSeconds),
            signingCredentials: creds
        );
    }
}
