using Bogus;
using Domain.Entities;

namespace Infrastructure.Seed;

public static class FakeClienteGenerator
{
    public static List<Cliente> Generate(int count = 10)
{
    return new Faker<Cliente>()
        .RuleFor(c => c.NumeroDocumento, f => f.Random.ReplaceNumbers("########"))
        .RuleFor(c => c.Nombre, f => f.Name.FirstName())
        .RuleFor(c => c.Apellido, f => f.Name.LastName())
        .RuleFor(c => c.Edad, f => f.Random.Short(18, 80))
        .RuleFor(c => c.Telefono, f => f.Phone.PhoneNumber("###-###-####"))
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Reservas, _ => new List<Reserva>())
        .Generate(count);
}

}
