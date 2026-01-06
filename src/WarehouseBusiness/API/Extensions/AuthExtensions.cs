using System.Text;
using API.Common;
using Application.Helper.Options;
using Contract.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  public static class AuthExtensions
  {
    public static IServiceCollection AddAuthenticationByJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddOptions<JwtOptions>()
              .Bind(configuration.GetSection("JWT"))
              .ValidateDataAnnotations()
              .Validate(o => !string.IsNullOrWhiteSpace(o.SecretKey), "JWT:SECRET_KEY is required!")
              .ValidateOnStart();

      var jwt = configuration.GetSection("JWT").Get<JwtOptions>()
               ?? throw new InvalidOperationException("JWT configuration section is missing");
      var keyBytes = Encoding.UTF8.GetBytes(jwt.SecretKey);

      services
          .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = jwt.Issuer,
              ValidAudience = jwt.Audience,
              IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
            };

            options.Events = new JwtBearerEvents
            {
              OnChallenge = async context =>
              {
                context.HandleResponse();
                await Writer.WriteProblem(context, StatusCodes.Status401Unauthorized, ApiErrorCode.Unauthorized);
              },

              OnForbidden = async context =>
              {
                await Writer.WriteProblem(context, StatusCodes.Status403Forbidden, ApiErrorCode.Forbidden);
              }
            };
          });
      return services;
    }

    public static IApplicationBuilder AddAppHeaders(this IApplicationBuilder app)
    {
      app.Use(async (httpContext, next) =>
      {
        httpContext.Response.Headers.XContentTypeOptions = "nosniff"; //MIME-type sniffing
        httpContext.Response.Headers.XFrameOptions = "DENY"; //Clickjacking attack
        httpContext.Response.Headers["Referrer-Policy"] = "no-referrer";
        httpContext.Response.Headers["Permissions-Policy"] = "geolocation=()";//disable GPS
        await next();
      });
      return app;
    }
  }
}