using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Common;

public static class ApiBuilder
{
    public static IActionResult Result<T>(ApiResponse<T>? apiResponse = default)
    {
        if (apiResponse is null)
        {
            return new NoContentResult();
        }

        return new ObjectResult(apiResponse.Result)
        {
            StatusCode = apiResponse.StatusCode,
        };
    }
}
