using Bogus;
using Domain.Entities;

namespace Infrastructure.Seed;

public static class FakeSalaGenerator
{
    public static List<Sala> Generate(int count = 5)
    {
        return new Faker<Sala>()
            .RuleFor(s => s.Nombre, f => $"Sala {f.Commerce.Department()}")
            .RuleFor(s => s.Numero, f => f.Random.Short(1, 20))
            .RuleFor(s => s.Carteleras, _ => new List<Cartelera>())
            .RuleFor(s => s.Asientos, _ => new List<Asiento>())
            .Generate(count);
    }
}
