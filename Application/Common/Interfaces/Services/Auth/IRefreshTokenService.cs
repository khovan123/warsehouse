namespace Application;

public interface IRefreshTokenService
{
    bool ValidateRefreshToken(string rawRefreshToken, string storedHash);
    string HashRefreshToken(string refreshToken);
    string GenerateRefreshToken();
}
