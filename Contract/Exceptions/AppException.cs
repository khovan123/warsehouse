namespace Contract;

public abstract class AppException : Exception
{
    public virtual string Type { get; }
    public virtual string Title { get; }
    public virtual int Status { get; }
    public virtual IDictionary<string, string[]>? Errors { get; }

    protected AppException(
    string message,
    string? type = null,
    int? status = null,
    string? title = null,
    IDictionary<string, string[]>? errors = null) : base(message)
    {
        Type = type ?? "INTERNAL_ERROR";
        Status = status ?? 500;
        Title = title ?? "Unexpected Error";
        Errors = errors;
    }
}