using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Application.Interfaces;
using Dsw2026Ej15.Application.Services;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Persistence;
using Dsw2026Ej15.Data.Seeders;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Server=localhost;Database=dsw2026;User Id=SA;Password=Matiasww42yi;TrustServerCertificate=True";

            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersistence, PersistenceEF>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddHealthChecks();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
                var seeder = new SpecialitySeeder(context);
                await seeder.SeedAsync("Sources/specialities.json");
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health-check");
            app.Run();
        }

    }
}
