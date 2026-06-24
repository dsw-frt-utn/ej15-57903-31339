using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Persistence;

public class PersistenceEF : IPersistence
{
    private Dsw2026Ej15DbContext _context;

    public PersistenceEF(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public async Task SaveDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
    {
        return await _context.Doctors.Include(d => d.Speciality).FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();
    }

    public async Task<bool> DeactivateDoctorAsync(Guid id)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        if (doctor == null) return false;
        doctor.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
    {
        return await _context.Specialities.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Speciality>> GetAllSpecialtiesAsync()
    {
        return await _context.Specialities.ToListAsync();
    }
}
