using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        await _doctorService.CreateDoctorAsync(request.Name, request.LicenseNumber, request.SpecialityId);
        return Created();
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    [HttpGet("doctors/{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        return Ok(doctor);
    }

    [HttpDelete("doctors/{id:guid}")]
    public async Task<IActionResult> DeactivateDoctor(Guid id)
    {
        await _doctorService.DeactivateDoctorAsync(id);
        return NoContent();
    }
}
