namespace Application;

public interface ISecurityTokenService
{
    string GenerateToken();
    //string HashResetToken(string token);
    //bool ValidateHashedResetToken(string token, string hashedToken);
}
