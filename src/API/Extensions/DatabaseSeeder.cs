using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CineDbContext>();

        // Reset completo solo en desarrollo
        if (context.Database.IsSqlServer() && context.Database.CanConnect())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        if (!await context.Clientes.AnyAsync())
        {
            var clientes = FakeClienteGenerator.Generate(50);
            await context.Clientes.AddRangeAsync(clientes);
            await context.SaveChangesAsync();
        }

        if (!await context.Peliculas.AnyAsync())
        {
            var peliculas = FakePeliculaGenerator.Generate(10);
            await context.Peliculas.AddRangeAsync(peliculas);
            await context.SaveChangesAsync();
        }

        /*if (!await context.Salas.AnyAsync())
        {
            var salas = FakeSalaGenerator.Generate(5);            
            
            await context.Salas.AddRangeAsync(salas);
            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();
        }

        if (!await context.Asientos.AnyAsync())
        {
            var salaIds = await context.Salas.AsNoTracking().Select(s => s.Id).ToListAsync();
            var allAsientos = new List<Asiento>();
            
            foreach (var salaId in salaIds)
            {
                var asientos = FakeAsientoGenerator.Generate(salaId);
                allAsientos.AddRange(asientos);
                context.ChangeTracker.Clear();
            }
            
            await context.Asientos.AddRangeAsync(allAsientos);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }*/

        var salas = FakeSalaGenerator.Generate(5);
        await context.Salas.AddRangeAsync(salas);
        await context.SaveChangesAsync();

        foreach (var sala in salas)
        {
            var asientos = FakeAsientoGenerator.Generate(sala.Id);
            
            await context.Asientos.AddRangeAsync(asientos);
        }
        
        await context.SaveChangesAsync();

        if (!await context.Carteleras.AnyAsync())
        {
            var peliculaIds = await context.Peliculas.AsNoTracking().Select(p => p.Id).ToListAsync();
            var salaIds = await context.Salas.AsNoTracking().Select(s => s.Id).ToListAsync();
            var carteleras = FakeCarteleraGenerator.Generate(peliculaIds, salaIds, 20);

            await context.Carteleras.AddRangeAsync(carteleras);
            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();
        }

        if (!await context.Reservas.AnyAsync())
        {
            // Obtener datos con tracking controlado
            var clientes = await context.Clientes.AsNoTracking().ToListAsync();
            var asientos = await context.Asientos.Where(a => a.Status).AsNoTracking().ToListAsync();
            var carteleras = await context.Carteleras.AsNoTracking().ToListAsync();

            if (clientes.Any() && asientos.Any() && carteleras.Any())
            {
                var reservas = new List<Reserva>();
                
                foreach (var cliente in clientes.Take(30)) // Limitar clientes para no exceder asientos
                {
                    var asiento = asientos.FirstOrDefault();
                    if (asiento == null) break;
                    
                    var cartelera = carteleras.OrderBy(c => c.Fecha).First();
                    
                    reservas.Add(new Reserva
                    {
                        FechaReserva = DateTime.Now.AddDays(-2),
                        ClienteId = cliente.Id,
                        AsientoId = asiento.Id,
                        CarteleraId = cartelera.Id,
                        Cliente = null!,
                        Asiento = null!,
                        Cartelera = null!
                    });
                    
                    asientos.Remove(asiento); // Evitar duplicados
                }
                
                context.ChangeTracker.Clear();
                await context.Reservas.AddRangeAsync(reservas);
                
                var asientosIds = reservas.Select(r => r.AsientoId).ToList();
                var asientosParaActualizar = await context.Asientos
                    .Where(a => asientosIds.Contains(a.Id))
                    .ToListAsync();
                    
                foreach (var asiento in asientosParaActualizar)
                {
                    asiento.Status = false;
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}