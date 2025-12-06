using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Common;

public static class ApiBuilder
{
    public static ObjectResult Result<T>(ApiResponse<T> apiResponse)
        => apiResponse switch
        {
            ApiResponse<T>.SuccessBuilder s => new ObjectResult(s.Result) { StatusCode = s.StatusCode },

            ApiResponse<T>.FailedBuilder f => new ObjectResult(f.Error) { StatusCode = f.StatusCode },

            _ => new ObjectResult(apiResponse)
        };
}
