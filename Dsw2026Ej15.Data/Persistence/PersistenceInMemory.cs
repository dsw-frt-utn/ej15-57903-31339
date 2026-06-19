namespace Dsw2026Ej15.Data.Persistence;

using System.Text.Json;
using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Domain.Interface;

public class PersistenceInMemory : IPersistence
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
    public void SaveDoctor(Doctor doctor)
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

    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialties.SingleOrDefault(s => s.Id == id);
    }

    public List<Speciality> GetAllSpecialties()
    {
        return _specialties;
    }



}