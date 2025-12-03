using System.Net;

namespace Contract;

public class ApiResponse<T>
{
    public string Type { get; set; } = "Success";
    public string Title { get; set; } = "SUCCESS";
    public int Status { get; set; } = (int)HttpStatusCode.OK;
    public string Detail { get; set; } = "Operation success";

    public IDictionary<string, string[]> Errors { get; set; }
        = new Dictionary<string, string[]>();

    public T? Data { get; set; }

    public static ApiResponse<T> Ok(string message)
    {
        return new ApiResponse<T>
        {
            Detail = message,
            Data = default
        };
    }

    public static ApiResponse<T> Ok(T data, string detail = "Success")
    {
        return new ApiResponse<T>
        {
            Detail = detail,
            Data = data
        };
    }

    public static ApiResponse<T> Error(string type, string title, int status, string detail,
        IDictionary<string, string[]>? errors = null)
    {
        return new ApiResponse<T>
        {
            Type = type,
            Title = title,
            Status = status,
            Detail = detail,
            Errors = errors ?? new Dictionary<string, string[]>(),
            Data = default
        };
    }
}