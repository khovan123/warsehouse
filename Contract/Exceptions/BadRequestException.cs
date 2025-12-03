namespace Contract;

public sealed class BadRequestException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Type => "BAD_REQUEST";
    public override string Title => "Bad Request";
    public override int Status => (int)HttpStatusCode.BadRequest;
}
