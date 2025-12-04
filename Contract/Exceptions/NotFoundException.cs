namespace Contract;

public sealed class NotFoundException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Title => "Not Found";
    public override string Type => "NOT_FOUND";
    public override int Status => (int)HttpStatusCode.NotFound;
}