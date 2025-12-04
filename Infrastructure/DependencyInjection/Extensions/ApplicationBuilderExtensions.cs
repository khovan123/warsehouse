using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Console.WriteLine("🔄 Applying migrations...");
        Console.WriteLine($">>> [Connection string:] {db.Database.GetConnectionString()}");
        await db.Database.MigrateAsync();

        if (app.Environment.IsDevelopment())
        {
            //var hasData = await db.Roles.AnyAsync();
            //if (!hasData)
            //{
            //    Console.WriteLine(">>> [[[Seeding development data]]]");
            //    await AppData.SeedAsync(db);
            //    Console.WriteLine(">>> >>> Seeding completed!");
            //}
            //else
            //{
            //    Console.WriteLine(">>> [[Database already seeded — skipping.]]]");
            //}
        }
    }
}