using Dsw2026Ej15.Domain.Entities;
public class Speciality : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    private Speciality() { }

    public Speciality(string name, string description, Guid? id = null) : base(id)
    {
        Name = name;
        Description = description;
    }
}