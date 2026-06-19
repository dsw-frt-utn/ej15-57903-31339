using Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; set; }
    public string LicenseNumber { get; set; }
    public bool IsActive { get; set; }
    public Specialty Specialty { get; set; }

    public Doctor(string name, string licenseNumber, Specialty specialty, Guid? id = null) : base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Specialty = specialty;
        IsActive = true;
    }
}