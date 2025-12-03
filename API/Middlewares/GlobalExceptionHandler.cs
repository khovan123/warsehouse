internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occured");

        if (exception is AppException appEx)
        {
            httpContext.Response.StatusCode = appEx.Status;
            var response = ApiResponse<object>.Error(
                type: appEx.Type,
                title: appEx.Title,
                status: appEx.Status,
                detail: appEx.Message,
                errors: appEx.Errors
            );

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var genericError = ApiResponse<object>.Error(
            type: "INTERNAL_ERROR",
            title: "Unexpected Error",
            status: StatusCodes.Status500InternalServerError,
            detail: exception.Message
        );
        await httpContext.Response.WriteAsJsonAsync(genericError, cancellationToken);
        return true;
    }
}