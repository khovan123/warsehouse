using System.Threading.RateLimiting;
using API.Common;
using AspNetCoreRateLimit;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
  public static class RateLimitExtensions
  {
    public static IServiceCollection AddApiRateLimit(this IServiceCollection services)
    {
      services.AddRateLimiter(options =>
      {
        options.OnRejected = async (context, ct) =>
        {
          context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
          context.HttpContext.Response.ContentType = "application/json";

          var problem = new ProblemDetails
          {
            Status = StatusCodes.Status429TooManyRequests,
            Title = ApiErrorCode.TooManyRequests,
            Instance = context.HttpContext.Request.Path
          };

          await context.HttpContext.Response.WriteAsJsonAsync(
            problem, ct
          );
        };

        options.AddPolicy("ip", httpContext =>
        //options.AddPolicy("ip-token", httpContext =>
        {
          var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
          var endpoint = httpContext.Request.Path.Value ?? "unknown";

          int permitLimit = 60;
          TimeSpan timer = TimeSpan.FromMinutes(2);
          QueueProcessingOrder processingOrder = QueueProcessingOrder.OldestFirst;
          int queueLimit = 0;

          if (endpoint.IsRegexMatch(Constants.REGEX_OTP_ENDPOINT))
          {
            permitLimit = 10;
          }
          else if (endpoint.IsRegexMatch(Constants.REGEX_SEARCH_ENDPOINT))
          {
            permitLimit = 120;
            timer = TimeSpan.FromMinutes(1);
          }
          ;

          //return RateLimitPartition.GetTokenBucketLimiter(ip, factory =>
          //{
          //  return new TokenBucketRateLimiterOptions { 
          //    AutoReplenishment = true,
          //    QueueLimit = 0,
          //    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
          //    ReplenishmentPeriod = TimeSpan.FromMinutes(1),
          //    TokenLimit = 60,
          //    TokensPerPeriod = 30,
          //  };
          //});

          return RateLimitPartition.GetFixedWindowLimiter(ip, factory =>
          {
            return new FixedWindowRateLimiterOptions
            {
              PermitLimit = permitLimit,
              Window = timer,
              QueueProcessingOrder = processingOrder,
              QueueLimit = queueLimit,
            };
          });
        });

        options.AddPolicy("route", httpContext =>
        //options.AddPolicy("route-token", httpContext =>
        {
          var endpoint = httpContext.GetEndpoint()?.DisplayName ?? "unknown";
          int permitLimit = 200;
          TimeSpan timer = TimeSpan.FromMinutes(1);
          QueueProcessingOrder processingOrder = QueueProcessingOrder.OldestFirst;
          int queueLimit = 10;

          if (endpoint.IsRegexMatch(Constants.REGEX_OTP_ENDPOINT))
          {
            permitLimit = 50;
            queueLimit = 0;
          }
          else if (endpoint.IsRegexMatch(Constants.REGEX_SEARCH_ENDPOINT))
          {
            permitLimit = 500;
            queueLimit = 0;
          }
          ;

          //return RateLimitPartition.GetTokenBucketLimiter(ip, factory =>
          //{
          //  return new TokenBucketRateLimiterOptions { 
          //    AutoReplenishment = true,
          //    QueueLimit = 0,
          //    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
          //    ReplenishmentPeriod = TimeSpan.FromMinutes(1),
          //    TokenLimit = 120,
          //    TokensPerPeriod = 60,
          //  };
          //});

          return RateLimitPartition.GetFixedWindowLimiter(endpoint, factory =>
          {
            return new FixedWindowRateLimiterOptions
            {
              PermitLimit = permitLimit,
              Window = timer,
              QueueLimit = queueLimit,
              QueueProcessingOrder = processingOrder,
            };
          });
        });

      });
      return services;
    }
  }
}