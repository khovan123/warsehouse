namespace Infrastructure;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly JwtOptions _jwt;

    public RefreshTokenService(IOptions<JwtOptions> options)
    {
        _jwt = options.Value;
    }

    public string GenerateRefreshToken()
        => TokenHelper.GenerateToken();

    public string HashRefreshToken(string refreshToken)
        => TokenHelper.HashToken(refreshToken, _jwt.RefreshTokenSecret);

    public bool ValidateRefreshToken(string rawRefreshToken, string storedHash)
        => TokenHelper.ValidateHashedToken(rawRefreshToken, storedHash, _jwt.RefreshTokenSecret);
}
