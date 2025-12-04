namespace Contract;

public class DatabaseOperationException(string message, IDictionary<string, string[]>? errors = null)
    : AppException(message, errors: errors)
{
    public override string Type => "DATABASE_OPERATION_FAILED";
    public override string Title => "Database operation failed";
    public override int Status => (int)HttpStatusCode.InternalServerError;
}