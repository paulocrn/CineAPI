using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Repositories
{
    public class RepositorioReservaIntegracionTests
    {
        private CineDbContext CrearContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<CineDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Nuevo contexto para cada test
                .Options;

            return new CineDbContext(options);
        }

        [Fact]
        public async Task ObtenerReservasDeTerrorPorRangoDeFechas_DebeRetornarSoloReservasDePeliculasDeTerror()
        {
            // Arrange
            using var context = CrearContextoInMemory();

            var peliculaTerror = new Pelicula { Id = 1, Genero = GeneroPelicula.Terror, Nombre = "La Maldición", Carteleras = new List<Cartelera>() };
            var peliculaAccion = new Pelicula { Id = 2, Genero = GeneroPelicula.Accion, Nombre = "Explosion Total", Carteleras = new List<Cartelera>() };

            var sala = new Sala { Id = 1, Nombre = "Sala 1", Numero = 1, Carteleras = new List<Cartelera>(), Asientos = new List<Asiento>() };

            var carteleraTerror = new Cartelera
            {
                Id = 1,
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(18, 0, 0),
                HoraFin = new TimeSpan(20, 0, 0),
                Sala = sala,
                SalaId = sala.Id,
                Pelicula = peliculaTerror,
                PeliculaId = peliculaTerror.Id,
                Reservas = new List<Reserva>()
            };

            var carteleraAccion = new Cartelera
            {
                Id = 2,
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(14, 0, 0),
                HoraFin = new TimeSpan(16, 0, 0),
                Sala = sala,
                SalaId = sala.Id,
                Pelicula = peliculaAccion,
                PeliculaId = peliculaAccion.Id,
                Reservas = new List<Reserva>()
            };

            var cliente = new Cliente
            {
                Id = 1,
                Nombre = "Ana",
                Apellido = "Gómez",
                Edad = 25,
                Email = "ana@ejemplo.com",
                NumeroDocumento = "12345678",
                Telefono = "123456789",
                Reservas = new List<Reserva>()
            };

            var reservaTerror = new Reserva
            {
                Id = 1,
                FechaReserva = DateTime.Today,
                Cartelera = carteleraTerror,
                CarteleraId = carteleraTerror.Id,
                Cliente = cliente,
                ClienteId = cliente.Id,
                Asiento = new Asiento { Id = 1, Numero = 10, Fila = 1, Sala = sala, SalaId = sala.Id, Status = true, Reservas = new List<Reserva>() },
                AsientoId = 1
            };

            var reservaAccion = new Reserva
            {
                Id = 2,
                FechaReserva = DateTime.Today,
                Cartelera = carteleraAccion,
                CarteleraId = carteleraAccion.Id,
                Cliente = cliente,
                ClienteId = cliente.Id,
                Asiento = new Asiento { Id = 2, Numero = 5, Fila = 2, Sala = sala, SalaId = sala.Id, Status = true, Reservas = new List<Reserva>() },
                AsientoId = 2
            };

            await context.Peliculas.AddRangeAsync(peliculaTerror, peliculaAccion);
            await context.Salas.AddAsync(sala);
            await context.Carteleras.AddRangeAsync(carteleraTerror, carteleraAccion);
            await context.Clientes.AddAsync(cliente);
            await context.Reservas.AddRangeAsync(reservaTerror, reservaAccion);
            await context.Asientos.AddRangeAsync(reservaTerror.Asiento, reservaAccion.Asiento);
            await context.SaveChangesAsync();

            var repositorio = new RepositorioReserva(context);

            var fechaInicio = DateTime.Today.AddDays(-1);
            var fechaFin = DateTime.Today.AddDays(1);

            // Act
            var resultado = await context.Reservas
                .Include(r => r.Cartelera)
                    .ThenInclude(c => c.Pelicula)
                .Where(r => r.Cartelera.Pelicula.Genero == GeneroPelicula.Terror
                         && r.FechaReserva >= fechaInicio
                         && r.FechaReserva <= fechaFin)
                .ToListAsync();

            // Assert
            Assert.Single(resultado);
            Assert.Equal(GeneroPelicula.Terror, resultado[0].Cartelera.Pelicula.Genero);
            Assert.Equal("La Maldición", resultado[0].Cartelera.Pelicula.Nombre);
        }
    }
}