using AspNetCoreRateLimit;
using System.Text.Json;

namespace API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Filter
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        // Middleware
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Rate Limiting
        services.AddRateLimitingServices(configuration);

        return services;
    }

    public static IServiceCollection AddRateLimitingServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Load rate limit configuration from appsettings.json
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        // Add MemoryCache (required by AspNetCoreRateLimit)
        services.AddMemoryCache();

        // Add IP rate limiting services
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
