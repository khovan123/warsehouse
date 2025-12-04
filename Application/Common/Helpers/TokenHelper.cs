using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Application;

public static class TokenHelper
{
    public static string GenerateToken(int size = 64)
    {
        byte[] bytes = new byte[size];
        RandomNumberGenerator.Fill(bytes);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static string HashToken(string token, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        Span<byte> hashBytes = stackalloc byte[32];
        hmac.TryComputeHash(Encoding.UTF8.GetBytes(token), hashBytes, out _);
        return Convert.ToHexString(hashBytes);
    }

    public static bool ValidateHashedToken(string token, string hashedToken, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

        Span<byte> computed = stackalloc byte[32];
        hmac.TryComputeHash(Encoding.UTF8.GetBytes(token), computed, out _);

        byte[] stored;
        try { stored = Convert.FromHexString(hashedToken); }
        catch { return false; }

        return CryptographicOperations.FixedTimeEquals(computed, stored);
    }
}