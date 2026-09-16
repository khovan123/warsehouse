namespace Contract;

public sealed class TooManyRequestException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Title => "Too Many Request";
    public override string Type => "TOO_MANY_REQUEST";
    public override int Status => (int)HttpStatusCode.TooManyRequests;
}