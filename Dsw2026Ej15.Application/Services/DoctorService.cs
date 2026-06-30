using Dsw2026Ej15.Application.Dtos;
using Dsw2026Ej15.Application.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interface;

namespace Dsw2026Ej15.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IRepository<Speciality> _specialityRepository;

    public DoctorService(IDoctorRepository doctorRepository, IRepository<Speciality> specialityRepository)
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
    }

    public async Task CreateDoctorAsync(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("El nombre es requerido");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("La matricula es requerida");

        var speciality = await _specialityRepository.GetByIdAsync(request.SpecialityId)
            ?? throw new ValidationException("La especialidad indicada no existe");

        await _doctorRepository.AddAsync(new Doctor(request.Name, request.LicenseNumber, speciality));
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepository.GetAllActiveAsync();
        return doctors.Select(d => new DoctorDto(d.Name, d.LicenseNumber, d.Speciality.Name));
    }

    public async Task<DoctorDto> GetDoctorByIdAsync(Guid id)
    {
        var doctor = await _doctorRepository.GetActiveByIdAsync(id)
            ?? throw new NotFoundException("El médico no existe o no está activo");
        return new DoctorDto(doctor.Name, doctor.LicenseNumber, doctor.Speciality.Name);
    }

    public async Task DeactivateDoctorAsync(Guid id)
    {
        var doctor = await _doctorRepository.GetActiveByIdAsync(id)
            ?? throw new NotFoundException("El médico no existe o no está activo");

        await _doctorRepository.DeactivateAsync(doctor.Id);
    }
}
