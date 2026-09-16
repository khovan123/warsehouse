namespace Contract;

public sealed class ForbiddenException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Title => "Forbidden";
    public override string Type => "FORBIDDEN";
    public override int Status => (int)HttpStatusCode.Forbidden;
}