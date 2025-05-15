using Bogus;
using Domain.Entities;

namespace Infrastructure.Seed;

public static class FakeCarteleraGenerator
{
    public static List<Cartelera> Generate(List<int> peliculaIds, List<int> salaIds, int count = 10)
    {
        var faker = new Faker<Cartelera>()
            .RuleFor(c => c.Fecha, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(30)))
            .RuleFor(c => c.HoraInicio, f => 
            {
                var time = f.Date.BetweenTimeOnly(new TimeOnly(10, 0), new TimeOnly(22, 0));
                return new TimeSpan(time.Hour, time.Minute, 0);
            })
            .RuleFor(c => c.HoraFin, (f, c) => 
            {
                var maxDuration = TimeSpan.FromHours(24).Subtract(TimeSpan.FromTicks(1)).Subtract(c.HoraInicio);
                var duration = TimeSpan.FromMinutes(f.Random.Int(90, 180));
                
                return c.HoraInicio.Add(duration <= maxDuration ? duration : maxDuration);
            })
            .RuleFor(c => c.PeliculaId, f => f.PickRandom(peliculaIds))
            .RuleFor(c => c.SalaId, f => f.PickRandom(salaIds))
            .RuleFor(c => c.Reservas, _ => new List<Reserva>());

        return faker.Generate(count);
    }
}