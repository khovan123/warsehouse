using API.DependencyInjection;
using API.Extensions;
using API.Middlewares;
using Infrastructure.DB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiRateLimit();

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

builder.Services.AddMongoDbHealthCheck();

builder.Services.AddAuthenticationByJwtBearer(builder.Configuration);

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
app.UseMiddleware<CorrelationCheckMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Default");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/api/v1/health/live").DisableRateLimiting();
app.MapHealthChecks("/api/v1/health/ready").DisableRateLimiting();

app.MapControllers().RequireRateLimiting("ip");
//app.MapControllers().RequireRateLimiting("ip-token");


app.Run();
