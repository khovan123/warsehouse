using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        AddInfrastructureDbContext(services, configuration);
        AddRedisCache(services, configuration);
        AddAuthenticationServices(services, configuration);
        //AddQuartzService(services, configuration);

        return services;
    }

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register JWT options
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        // Email
        services.AddScoped<IEmailSender, EmailSender>();

        // Register authentication services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtTokensService, JwtTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ISecurityTokenService, SecurityTokenService>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        return services;
    }

    public static IServiceCollection AddInfrastructureDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<TimestampInterceptor>();

        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            options.AddInterceptors(sp.GetRequiredService<TimestampInterceptor>());
        });
        return services;
    }

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["Upstash:BaseUrl"] ?? throw new InvalidOperationException("Upstash:BaseUrl is required");
        var token = configuration["Upstash:Token"] ?? throw new InvalidOperationException("Upstash:Token is required");

        services.AddHttpClient<IRedisCacheService, RedisCacheService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        })
            .ConfigurePrimaryHttpMessageHandler(() =>
                new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                    MaxConnectionsPerServer = 100,
                    UseProxy = false,
                    Proxy = null
                });

        return services;
    }
    //public static IServiceCollection AddQuartzService(this IServiceCollection services, IConfiguration configuration)
    //{

    //    services.AddQuartz(q =>
    //    {
    //        q.SchedulerId = "AUTO";
    //        q.SchedulerName = "DigitalYouthHandbookScheduler";

    //        q.SetProperty("quartz.serializer.type", "json");

    //        q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 5; });

    //        q.UsePersistentStore(store =>
    //        {
    //            var quartzConn = configuration.GetConnectionString("QuartzConnection")
    //                             ?? throw new InvalidOperationException("Missing QuartzConnection in appsettings.json");

    //            store.UseSqlServer(quartzConn);
    //            store.UseClustering();
    //            store.UseProperties = true;
    //            store.RetryInterval = TimeSpan.FromSeconds(15);
    //        });

    //        // Đăng ký SendEmailJob với DI support
    //        q.AddJob<SendEmailJob>(opts => opts
    //            .WithIdentity("SendEmailJob")
    //            .StoreDurably());
    //    });

    //    // Đăng ký Quartz để sử dụng DI container
    //    services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

    //    services.AddScoped<IBackgroundJobService, QuartzBackgroundJobService>();

    //    return services;
    //}

    public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        var jwt = configuration.GetSection("Jwt").Get<JwtOptions>()!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.EventsType = typeof(CustomJwtBearerEvents);
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("access_token"))
                        {
                            context.Token = context.Request.Cookies["access_token"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }
}

