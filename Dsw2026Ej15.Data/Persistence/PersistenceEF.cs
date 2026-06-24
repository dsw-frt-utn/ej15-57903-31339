using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Persistence;

public class PersistenceEF : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;

    public PersistenceEF(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public void SaveDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _context.Doctors.Include(d => d.Speciality).FirstOrDefault(d => d.Id == id && d.IsActive);
    }

    public List<Doctor> GetAllDoctors()
    {
        return _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToList();
    }

    public bool DeactivateDoctor(Guid id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        if (doctor == null) return false;
        doctor.IsActive = false;
        _context.SaveChanges();
        return true;
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _context.Specialities.FirstOrDefault(s => s.Id == id);
    }

    public List<Speciality> GetAllSpecialties()
    {
        return _context.Specialities.ToList();
    }
}
