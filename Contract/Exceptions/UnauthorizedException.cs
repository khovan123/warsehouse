namespace Contract;

public sealed class UnauthorizedException(string message, IDictionary<string, string[]>? errors = null)
: AppException(message, errors: errors)
{
    public override string Title => "Unauthorized";
    public override string Type => "UNAUTHORIZED";
    public override int Status => (int)HttpStatusCode.Unauthorized;
}