namespace Dsw2026Ej15.Domain.Abstractions;
using Dsw2026Ej15.Domain.Entities;

public interface IPersistence
{
    void AddDoctor(Doctor doctor);
    
    Doctor? GetDoctorById(Guid id);
    List<Doctor> GetAllDoctors();
    bool DeactivateDoctor(Guid id);

    Specialty? GetSpecialtyById(Guid id);
    List<Specialty> GetAllSpecialties();
}
