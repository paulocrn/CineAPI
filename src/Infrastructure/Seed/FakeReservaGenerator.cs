// Infrastructure/Seed/FakeReservaGenerator.cs
using Bogus;
using Domain.Entities;

namespace Infrastructure.Seed;

public static class FakeReservaGenerator
{
    public static List<Reserva> Generate(
        List<Cliente> clientes, 
        List<Asiento> asientos,
        List<Cartelera> carteleras,
        int count)
    {
        var faker = new Faker<Reserva>()
            .RuleFor(r => r.FechaReserva, f => f.Date.Between(DateTime.Today.AddDays(-7), DateTime.Today))
            .RuleFor(r => r.ClienteId, f => f.PickRandom(clientes).Id)
            .RuleFor(r => r.AsientoId, f => f.PickRandom(asientos).Id)
            .RuleFor(r => r.CarteleraId, f => f.PickRandom(carteleras).Id);

        return faker.Generate(count);
    }
}
