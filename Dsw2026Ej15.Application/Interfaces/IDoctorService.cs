using Dsw2026Ej15.Application.Dtos;

namespace Dsw2026Ej15.Application.Interfaces;

public interface IDoctorService
{
    Task CreateDoctorAsync(string name, string licenseNumber, Guid specialityId);
    Task<List<DoctorDto>> GetAllDoctorsAsync();
    Task<DoctorDto> GetDoctorByIdAsync(Guid id);
    Task DeactivateDoctorAsync(Guid id);
}
