using Dsw2026Ej15.Application.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interface;

namespace Dsw2026Ej15.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IPersistence _persistence;

    public DoctorService(IPersistence persistence)
    {
        _persistence = persistence;
    }

    public async Task CreateDoctorAsync(string name, string licenseNumber, Guid specialityId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("El nombre es requerido");

        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new ValidationException("La matricula es requerida");

        var speciality = await _persistence.GetSpecialityByIdAsync(specialityId)
            ?? throw new ValidationException("La especialidad indicada no existe");

        var doctor = new Doctor(name, licenseNumber, speciality);
        await _persistence.SaveDoctorAsync(doctor);
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _persistence.GetAllDoctorsAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(Guid id)
    {
        return await _persistence.GetDoctorByIdAsync(id)
            ?? throw new NotFoundException("El médico no existe o no está activo");
    }

    public async Task DeactivateDoctorAsync(Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id)
            ?? throw new NotFoundException("El médico no existe o no está activo");

        await _persistence.DeactivateDoctorAsync(doctor.Id);
    }
}
