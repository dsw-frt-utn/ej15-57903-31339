using Dsw2026Ej15.Api.Configuration;
using Dsw2026Ej15.Api.Middlewares;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("La conexión 'DefaultConnection' no se encontró.");

            builder.Services.AddDatabase(connectionString);
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            await app.Services.SeedDatabaseAsync();

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
