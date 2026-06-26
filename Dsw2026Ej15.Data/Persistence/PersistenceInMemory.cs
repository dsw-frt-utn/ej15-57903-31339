namespace Dsw2026Ej15.Data.Persistence;

using System.Text.Json;
using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Domain.Interface;

public class PersistenceInMemory: IPersistence
{
    private List<Doctor> _doctors = new List<Doctor>();
    private List<Speciality> _specialties = new List<Speciality>();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }
    private void LoadSpecialities()
    {
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
        var json = File.ReadAllText(jsonPath);

        var specialties = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        if (specialties != null)
        {
            _specialties.AddRange(specialties.Select(s => new Speciality(s.Name, s.Description, s.Id)));
        }
    }
    public async Task SaveDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public async Task<Doctor?> GetDoctorById(Guid id)
    {
        return _doctors.SingleOrDefault(d => d.Id == id);
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return _doctors.Where(d => d.IsActive).ToList();
    }

    public async Task<bool> DeactivateDoctor(Guid id)
    {
        var doctor = await GetDoctorById(id);
        if (doctor != null)
        {
            doctor.IsActive = false;
            return true;
        }
        return false;
    }

    public async Task<Speciality?> GetSpecialityById(Guid id)
    {
        return _specialties.SingleOrDefault(s => s.Id == id);
    }

    public async Task<IEnumerable<Speciality>> GetAllSpecialties()
    {
        return _specialties;
    }

}