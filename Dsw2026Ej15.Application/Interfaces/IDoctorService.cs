using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Application.Interfaces;

public interface IDoctorService
{
    Task CreateDoctorAsync(string name, string licenseNumber, Guid specialityId);
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<Doctor> GetDoctorByIdAsync(Guid id);
    Task DeactivateDoctorAsync(Guid id);
}
