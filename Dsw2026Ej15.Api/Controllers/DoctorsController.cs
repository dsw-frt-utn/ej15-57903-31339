using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;


public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }
    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("El nombre es requerido");
        }
        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("La matricula es requerdida");
        }

        var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);

        if (speciality is null)
        {
            throw new ValidationException("La especialidad indicada no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        await _persistence.SaveDoctorAsync(doctor);

        return Created();
    }


    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _persistence.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    [HttpGet("doctors/{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id) ?? throw new NotFoundException("El médico no existe o no está activo");

        return Ok(new { doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name });
    }

    [HttpDelete("doctors/{id:guid}")]
    public async Task<IActionResult> DeactivateDoctor(Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id) ?? throw new NotFoundException("El médico no existe o no está activo");
        await _persistence.DeactivateDoctorAsync(doctor.Id);
        return NoContent();
    }
}
