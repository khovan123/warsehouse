using API.Common;
using Application.Exceptions;
using Contract.Responses;
using FluentValidation;

namespace API.Middlewares;

public sealed class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
  {
    _next = next;
    _logger = logger;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (ValidationException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status422UnprocessableEntity,
          ex: ex,
          title: ApiErrorCode.ValidationFailed,
          detail: ex.Message,
          extensions: new Dictionary<string, object?>
          {
            ["errors"] = ex.Errors
                  .GroupBy(e => e.PropertyName)
                  .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())
          });
    }
    catch (UnauthorizedException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status401Unauthorized, ApiErrorCode.Unauthorized, ex, ex.Message);
    }
    catch (ForbiddenException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status403Forbidden, ApiErrorCode.Forbidden, ex, ex.Message);
    }
    catch (NotFoundException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status404NotFound, ApiErrorCode.NotFound, ex, ex.Message);
    }
    catch (ConflictException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status409Conflict, ApiErrorCode.Conflict, ex, ex.Message);
    }
    catch (TokenExpiredException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status401Unauthorized, ApiErrorCode.TokenExpired, ex, ex.Message);
    }
    catch (InvalidRefreshTokenException ex)
    {
      await Writer.WriteProblem(context, StatusCodes.Status401Unauthorized, ApiErrorCode.InvalidRefreshToken, ex, ex.Message);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, ApiErrorCode.UnHandledException);
      await Writer.WriteProblem(context, StatusCodes.Status500InternalServerError, ApiErrorCode.InternalServerError, ex, ex.Message);
    }
  }
}
