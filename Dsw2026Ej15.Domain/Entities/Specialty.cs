using Dsw2026Ej15.Domain.Entities;
public class Specialty : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Specialty(string name, string description, Guid? id = null) : base(id)
    {
        Name = name;
        Description = description;
    }
}