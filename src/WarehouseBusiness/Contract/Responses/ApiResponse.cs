using Microsoft.AspNetCore.Http;

namespace Contract.Responses;

public abstract record ApiResponse<T>
{
    public sealed record SuccessBuilder(T Data, string? Message = null, int? StatusCode = StatusCodes.Status200OK) : ApiResponse<T>
    {
        public Success Result { get; init; } = new Success(Data, Message);
        public int? StatusCode { get; init; } = StatusCode;
    }

    public sealed record FailedBuilder(string? Message, string? ErrorCode, int? StatusCode = StatusCodes.Status400BadRequest, object ? Errors = null) : ApiResponse<T>
    {
        public Failed Error { get; init; } = new Failed(Message, ErrorCode, Errors);
        public int? StatusCode { get; init; } = StatusCode;
    }
 
    public record Success(T Data, string? Message = null);
    public record Failed(string? Message, string? ErrorCode, object? Errors = null);
}
