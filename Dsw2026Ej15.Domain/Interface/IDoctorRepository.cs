using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interface;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<List<Doctor>> GetAllActiveAsync();
    Task<Doctor?> GetActiveByIdAsync(Guid id);
    Task DeactivateAsync(Guid id);
}
