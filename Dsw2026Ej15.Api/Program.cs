using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Application.Interfaces;
using Dsw2026Ej15.Application.Services;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Data.Persistence;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
                await SeedSpecialities(context);
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

        private static async Task SeedSpecialities(Dsw2026Ej15DbContext context)
        {
            if (await context.Specialities.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("Sources/specialities.json");
            var specialitiesDto = JsonSerializer.Deserialize<List<SpecialityDto>>(json);

            if (specialitiesDto == null) return;

            foreach (var dto in specialitiesDto)
            {
                context.Specialities.Add(new Speciality(dto.Name, dto.Description, dto.Id));
            }

            await context.SaveChangesAsync();
        }

    }
}


