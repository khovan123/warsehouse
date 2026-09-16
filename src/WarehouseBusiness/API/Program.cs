using API.DependencyInjection;
using API.Middlewares;
using Infrastructure.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        var origins = builder.Configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>();

        if (builder.Environment.IsDevelopment())
        {
            policy.WithOrigins(origins!)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
        else
        {
            policy.WithOrigins(origins!)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
    });
});


builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection("MONGO"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
    return new MongoClient(config.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(config.DatabaseName);
});

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddProjectDependencies();

builder.Services
    .AddHealthChecks()
    .AddMongoDb(
        clientFactory: sp => sp.GetRequiredService<IMongoClient>(),
        databaseNameFactory: sp =>
        {
            var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
            return config.DatabaseName;
        },
        name: "mongodb"
    );

var jwtSection = builder.Configuration.GetSection("JWT");
var keyBytes = Encoding.UTF8.GetBytes(jwtSection["SECRET_KEY"]!);

builder.Services
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

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Warehouse API");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapHealthChecks("/api/v1/health/live");
app.MapHealthChecks("/api/v1/health/ready");


app.UseHttpsRedirection();
app.UseCors("Default");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
