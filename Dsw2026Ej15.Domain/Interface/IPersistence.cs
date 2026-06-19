namespace Dsw2026Ej15.Domain.Interface;
using Dsw2026Ej15.Domain.Entities;

public interface IPersistence
{
    void SaveDoctor(Doctor doctor);

    Doctor? GetDoctorById(Guid id);
    List<Doctor> GetAllDoctors();
    bool DeactivateDoctor(Guid id);

    Speciality? GetSpecialityById(Guid id);
    List<Speciality> GetAllSpecialties();
}
