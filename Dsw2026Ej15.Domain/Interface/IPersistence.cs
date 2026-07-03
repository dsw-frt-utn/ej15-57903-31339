namespace Dsw2026Ej15.Domain.Interface;
using Dsw2026Ej15.Domain.Entities;

public interface IPersistence
{
    Task SaveDoctor(Doctor doctor);
    Task UpdateDoctor(Doctor doctor);
    Task<Doctor?> GetDoctorById(Guid id);
    Task<IEnumerable<Doctor>> GetAllDoctors();
    Task<bool> DeactivateDoctor(Guid id);
    Task<Speciality?> GetSpecialityById(Guid id);
    Task<IEnumerable<Speciality>> GetAllSpecialties();
}
