using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Seeders;

public class SpecialitySeeder
{
    private Dsw2026Ej15DbContext _context;

    public SpecialitySeeder(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(string jsonPath)
    {
        if (await _context.Specialities.AnyAsync()) return;

        var json = await File.ReadAllTextAsync(jsonPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var specialitiesDto = JsonSerializer.Deserialize<List<SpecialityDto>>(json, options);

        if (specialitiesDto == null) return;

        foreach (var dto in specialitiesDto)
        {
            _context.Specialities.Add(new Speciality(dto.Name, dto.Description, dto.Id));
        }

        await _context.SaveChangesAsync();
    }
}
