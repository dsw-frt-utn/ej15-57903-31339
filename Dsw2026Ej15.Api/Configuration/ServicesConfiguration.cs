using Dsw2026Ej15.Application.Interfaces;
using Dsw2026Ej15.Application.Services;
using Dsw2026Ej15.Data.Persistence;
using Dsw2026Ej15.Domain.Interface;

namespace Dsw2026Ej15.Api.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHealthChecks();
        services.AddScoped<IPersistence, PersistenceEF>();
        services.AddScoped<IDoctorService, DoctorService>();

        return services;
    }
}
