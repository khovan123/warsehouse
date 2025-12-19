using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Common;

public static class ApiBuilder
{
    public static ObjectResult Result<T>(ApiResponse<T> apiResponse)
        => new ObjectResult(apiResponse.Result) { StatusCode = apiResponse.StatusCode };
}
