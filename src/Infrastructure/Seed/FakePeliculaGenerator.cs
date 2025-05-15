using Bogus;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Seed;

public static class FakePeliculaGenerator
{
    public static List<Pelicula> Generate(int count = 10)
    {
        var generos = Enum.GetValues<GeneroPelicula>();

        return new Faker<Pelicula>()
            .RuleFor(p => p.Nombre, f => f.Lorem.Sentence(3).TrimEnd('.'))
            .RuleFor(p => p.Genero, f => f.PickRandom(generos))
            .RuleFor(p => p.EdadMinimaPermitida, f => f.Random.Short(0, 18))
            .RuleFor(p => p.DuracionMinutos, f => f.Random.Short(60, 180))
            .RuleFor(p => p.Carteleras, _ => new List<Cartelera>())
            .Generate(count);
    }
}
