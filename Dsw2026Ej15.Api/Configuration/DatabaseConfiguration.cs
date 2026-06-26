using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configuration;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<Dsw2026Ej15DbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }

    public static async Task SeedDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
        await new SpecialitySeeder(context).SeedAsync("Sources/specialities.json");
    }
}
