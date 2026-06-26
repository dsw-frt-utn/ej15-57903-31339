using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeactivateDoctor(Guid id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
            if (doctor == null) return false;
            doctor.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Speciality>> GetAllSpecialties()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.Specialities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task SaveDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
