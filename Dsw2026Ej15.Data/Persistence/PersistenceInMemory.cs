namespace Dsw2026Ej15.Data.Persistence;

using System.Text.Json;
using Dsw2026Ej15.Data.dtos;
using Dsw2026Ej15.Domain.Abstractions;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> _doctors = new List<Doctor>();
    private List<Specialty> _specialties = new List<Specialty>();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }
    private void LoadSpecialities()
    {
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
        var json = File.ReadAllText(jsonPath);

        var specialties = JsonSerializer.Deserialize<List<SpecialtyDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        if (specialties != null)
        {
            _specialties.AddRange(specialties.Select(s => new Specialty(s.Name, s.Description, s.Id)));
        }
    }
    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _doctors.SingleOrDefault(d => d.Id == id);
    }

    public List<Doctor> GetAllDoctors()
    {
        return _doctors.Where(d => d.IsActive).ToList();
    }

    public bool DeactivateDoctor(Guid id)
    {
        var doctor = GetDoctorById(id);
        if (doctor != null)
        {
            doctor.IsActive = false;
            return true;
        }
        return false;
    }

    public Specialty? GetSpecialtyById(Guid id)
    {
        return _specialties.SingleOrDefault(s => s.Id == id);
    }

    public List<Specialty> GetAllSpecialties()
    {
        return _specialties;
    }



}