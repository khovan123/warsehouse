namespace Contract;

public class ConflictException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Type => "CONFLICT";
    public override string Title => "Conflict";
    public override int Status => (int)HttpStatusCode.Conflict;
}