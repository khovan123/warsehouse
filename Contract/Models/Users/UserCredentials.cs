namespace Contract;

public sealed class UserCredentials
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required bool IsVerified { get; set; }
}
