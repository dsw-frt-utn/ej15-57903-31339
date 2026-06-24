namespace Dsw2026Ej15.Domain.Interface;
using Dsw2026Ej15.Domain.Entities;

public interface IPersistence
{
    Task SaveDoctorAsync(Doctor doctor);
    Task<Doctor?> GetDoctorByIdAsync(Guid id);
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<bool> DeactivateDoctorAsync(Guid id);
    Task<Speciality?> GetSpecialityByIdAsync(Guid id);
    Task<List<Speciality>> GetAllSpecialtiesAsync();
}
