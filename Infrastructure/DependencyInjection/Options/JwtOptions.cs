namespace Infrastructure;

public class JwtOptions
{
    // Access Token
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int AccessTokenLifetime { get; set; }

    // Refresh Token
    public string RefreshTokenSecret { get; set; } = string.Empty;
    public int RefreshTokenLifetime { get; set; }
}

