using Microsoft.AspNetCore.Http;

namespace Contract.Responses
// UPDATE START
// public abstract record ApiResponse<T>
// {
//     public sealed record SuccessBuilder(T Data, string? Message = null, int? StatusCode = StatusCodes.Status200OK) : ApiResponse<T>
//     {
//         public Success Result { get; init; } = new Success(Data, Message);
//         public int? StatusCode { get; init; } = StatusCode;
//     }

//     public sealed record FailedBuilder(string? Message, string? ErrorCode, int? StatusCode = StatusCodes.Status400BadRequest, object? Errors = null) : ApiResponse<T>
//     {
//         public Failed Error { get; init; } = new Failed(Message, ErrorCode, Errors);
//         public int? StatusCode { get; init; } = StatusCode;
//     }

//     public record Success(T Data, string? Message = null);
//     public record Failed(string? Message, string? ErrorCode, object? Errors = null);
// }
{
    public record ApiResponse<TData>
    {
        public ApiResponse(
            TData data = default!,
            string? message = "Response successfully!",
            int? statusCode = StatusCodes.Status200OK)
        {
            Result = new Result<TData>(data, message);
            StatusCode = statusCode;
        }

        public Result<TData> Result { get; init; }

        public int? StatusCode { get; init; }

    }
    public record Result<TData>(TData Data = default!, string? Message = default!)
    {
        public TData Data { get; set; } = Data;
        public string? Message { get; set; } = Message;
    }
}
//UPDATE END

