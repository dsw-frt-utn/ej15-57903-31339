using Dsw2026Ej15.Application.Dtos;

namespace Dsw2026Ej15.Application.Interfaces;

public interface IDoctorService
{
    Task CreateDoctorAsync(DoctorModel.Request request);
    Task<List<DoctorDto>> GetAllDoctorsAsync();
    Task<DoctorDto> GetDoctorByIdAsync(Guid id);
    Task DeactivateDoctorAsync(Guid id);
}
