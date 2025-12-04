namespace Infrastructure;

public class SecurityTokenService : ISecurityTokenService
{
    private readonly JwtOptions _jwt;

    public SecurityTokenService(IOptions<JwtOptions> options)
    {
        _jwt = options.Value;
    }

    public string GenerateToken()
        => TokenHelper.GenerateToken();

    //public string HashResetToken(string token)
    //    => TokenHelper.HashToken(token, _jwt.PasswordResetSecret);

    //public bool ValidateHashedResetToken(string rawToken, string hashedTokenHex)
    //    => TokenHelper.ValidateHashedToken(rawToken, hashedTokenHex, _jwt.PasswordResetSecret);
}
