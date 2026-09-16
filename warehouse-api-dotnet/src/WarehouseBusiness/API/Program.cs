using Infrastructure.DB;
using API.DependencyInjection;
using API.Extensions;
using API.Middlewares;

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


builder.Services.AddMongoDBRunner(builder.Configuration);

builder.Services.AddPersistKeysToRedis(builder.Configuration, builder.Environment);

builder.Services.AddProjectDependencies();

builder.Services.AddMongoDbHealthCheck();

builder.Services.AddAuthenticationByJwtBearer(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await MongoIndexInitializer.EnsureIndexesAsync(ctx, CancellationToken.None);
}

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
else if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CorrelationCheckMiddleware>();

app.AddAppHeaders();

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
