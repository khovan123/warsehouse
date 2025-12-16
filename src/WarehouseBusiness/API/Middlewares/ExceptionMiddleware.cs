using Application.Exceptions;
using Contract.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public sealed class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly IHostEnvironment _enviroment;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
  {
    _next = next;
    _logger = logger;
    _enviroment = environment;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (ValidationException ex)
    {
      await WriteProblem(context, StatusCodes.Status422UnprocessableEntity,
          enviroment: _enviroment,
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
      await WriteProblem(context, StatusCodes.Status401Unauthorized, ApiErrorCode.Unauthorized, _enviroment, ex.Message);
    }
    catch (ForbiddenException ex)
    {
      await WriteProblem(context, StatusCodes.Status403Forbidden, ApiErrorCode.Forbidden, _enviroment, ex.Message);
    }
    catch (NotFoundException ex)
    {
      await WriteProblem(context, StatusCodes.Status404NotFound, ApiErrorCode.NotFound, _enviroment, ex.Message);
    }
    catch (ConflictException ex)
    {
      await WriteProblem(context, StatusCodes.Status409Conflict, ApiErrorCode.Conflict, _enviroment, ex.Message);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, ApiErrorCode.UnHandledException);
      await WriteProblem(context, StatusCodes.Status500InternalServerError, ApiErrorCode.InternalServerError, _enviroment, ex.Message);
    }
  }

  private static async Task WriteProblem(
      HttpContext context,
      int statusCode,
      string title,
      IHostEnvironment enviroment,
      string? detail = null,
      IDictionary<string, object?>? extensions = null)
  {
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/problem+json";


    var problem = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Instance = context.Request.Path
    };

    if (enviroment.IsDevelopment())
    {
      var traceId = context.TraceIdentifier;
      problem.Detail = detail;
      problem.Extensions["traceId"] = traceId;

      if (extensions is not null)
      {
        foreach (var kv in extensions)
          problem.Extensions[kv.Key] = kv.Value;
      }
    }
    else
    {
      problem.Detail = default;
    }

    await context.Response.WriteAsJsonAsync(problem);
  }
}
