using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioReserva : RepositorioBase<Reserva>, IRepositorioReserva
    {
        private readonly CineDbContext _contexto;
        public RepositorioReserva(CineDbContext context) : base(context) { 
            _contexto = context;
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorCarteleraAsync(int carteleraId)
        {
            return await _contexto.Reservas.Where(r => r.CarteleraId == carteleraId).ToListAsync();
        }


        public async Task<Reserva?> ObtenerPorClienteAsync(int clienteId)
        {
            return await _contexto.Reservas.FirstOrDefaultAsync(r => r.ClienteId == clienteId);
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasDeTerrorPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Reservas
                .Include(r => r.Cartelera)
                    .ThenInclude(c => c.Pelicula)
                .Where(r => r.Cartelera.Pelicula.Genero == GeneroPelicula.Terror
                        && r.FechaReserva >= fechaInicio
                        && r.FechaReserva <= fechaFin)
                .ToListAsync();
        }
    }
}