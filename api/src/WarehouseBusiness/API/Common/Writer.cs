using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace API.Common
{
  public static class Writer
  {
    public static async Task WriteProblem(
          HttpContext context,
          int statusCode,
          string title,
          Exception ex,
          string? detail = null,
          IDictionary<string, object?>? extensions = null
          )
    {
      if (context.Response.HasStarted && ex is not null)
      {
        throw ex;
      }
      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/problem+json";
      var traceId = context.TraceIdentifier;
      context.Response.Headers.Append("TraceId", traceId);

      await context.Response.WriteAsJsonAsync(WriteProblemJson(statusCode, title, context.Request.Path, traceId, detail, extensions));
    }

    public static async Task WriteProblem(
          JwtBearerChallengeContext context,
          int statusCode,
          string title,
          string? detail = null,
          IDictionary<string, object?>? extensions = null
          )
    {
      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/problem+json";

      await context.Response.WriteAsJsonAsync(WriteProblemJson(statusCode, title, context.Request.Path, null, detail, extensions));
    }

    public static async Task WriteProblem(
          ForbiddenContext context,
          int statusCode,
          string title,
          string? detail = null,
          IDictionary<string, object?>? extensions = null
          )
    {
      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/problem+json";
      await context.Response.WriteAsJsonAsync(WriteProblemJson(statusCode, title, context.Request.Path, null, detail, extensions));
    }

    public static ProblemDetails WriteProblemJson(
          int statusCode,
          string title,
          string? path = null,
          string? traceId = null,
          string? detail = null,
          IDictionary<string, object?>? extensions = null
          )
    {
      var problem = new ProblemDetails
      {
        Status = statusCode,
        Title = title,
        Instance = path,
        Detail = detail
      };

      problem.Extensions["traceId"] = traceId;

      if (extensions is not null)
      {
        foreach (var kv in extensions)
          problem.Extensions[kv.Key] = kv.Value;
      }
      return problem;
    }
  }
}