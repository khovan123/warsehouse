namespace Contract;

public sealed class SignInResponse
{
    public required string AccessToken { get; set; }
    public required int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}
