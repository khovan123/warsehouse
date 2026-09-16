using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  public static class AuthExtensions
  {
    public static IServiceCollection AddAuthenticationByJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtSection = configuration.GetSection("JWT");
      var keyBytes = Encoding.UTF8.GetBytes(jwtSection["SECRET_KEY"]!);

      services
          .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = jwtSection["ISSUER"],
              ValidAudience = jwtSection["AUDIENCE"],
              IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
            };

            options.Events = new JwtBearerEvents
            {
              OnChallenge = async context =>
              {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var payload = new
                {
                  success = false,
                  message = "Unauthorized",
                  errorCode = "UNAUTHORIZED",
                  statusCode = 401
                };

                await context.Response.WriteAsJsonAsync(payload);

              },

              OnForbidden = async context =>
              {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                var payload = new
                {
                  success = false,
                  message = "Forbidden",
                  errorCode = "FORBIDDEN",
                  statusCode = 403
                };

                await context.Response.WriteAsJsonAsync(payload);
              }
            };
          });
      return services;
    }
  }
}