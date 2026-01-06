using Serilog.Context;

namespace API.Middlewares
{
  public sealed class CorrelationCheckMiddleware
  {
    private const string HeaderName = "X-Correlation-id";
    private readonly RequestDelegate _next;

    public CorrelationCheckMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      var correlationId = context.Request.Headers.TryGetValue(HeaderName, out var result) && !string.IsNullOrEmpty(result) && !string.IsNullOrWhiteSpace(result) ? result.ToString() : Guid.NewGuid().ToString("N");

      context.Items[HeaderName] = correlationId;
      context.Response.Headers[HeaderName] = correlationId;

      using (LogContext.PushProperty("CorrelationId", correlationId))
      {
        await _next(context);
      }
    }
  }
}