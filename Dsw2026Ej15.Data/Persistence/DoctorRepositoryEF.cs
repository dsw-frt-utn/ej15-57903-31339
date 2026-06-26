using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Persistence;

public class DoctorRepositoryEF : RepositoryEF<Doctor>, IDoctorRepository
{
    public DoctorRepositoryEF(Dsw2026Ej15DbContext context) : base(context) { }

    public async Task<List<Doctor>> GetAllActiveAsync() =>
        await _dbSet.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();

    public async Task<Doctor?> GetActiveByIdAsync(Guid id) =>
        await _dbSet.Include(d => d.Speciality).SingleOrDefaultAsync(d => d.Id == id && d.IsActive);

    public async Task DeactivateAsync(Guid id)
    {
        var doctor = await _dbSet.SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        if (doctor == null) return;
        doctor.IsActive = false;
        await _context.SaveChangesAsync();
    }
}
