using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioCartelera : RepositorioBase<Cartelera>, IRepositorioCartelera
    {
        private readonly new CineDbContext _context;
        
        public RepositorioCartelera(CineDbContext context) : base(context)
        {
            _context = context;
        }

        public async new Task<Cartelera> ObtenerPorIdAsync(int id)
        {
            var cartelera = await _context.Carteleras
                .Include(c => c.Pelicula)
                .Include(c => c.Sala)
                    .ThenInclude(s => s.Asientos)
                .Include(c => c.Reservas)
                    .ThenInclude(r => r.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cartelera == null)
            {
                throw new Exception($"Cartelera no encontrada");
            }

            return cartelera;
        }

        public new async Task<IEnumerable<Cartelera>> ObtenerTodosAsync()
        {
            var cartelera = await _context.Carteleras
                .Include(c => c.Pelicula)
                .Include(c => c.Sala)
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.HoraInicio)
                .ToListAsync();

            if (cartelera == null)
            {
                throw new Exception($"Cartelera no encontrada");
            }

            return cartelera;
        }

        public async Task<IEnumerable<Reserva>> ObtenerReservasPorGeneroYRangoFechas(GeneroPelicula genero, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Reservas
                .Include(r => r.Cartelera)
                    .ThenInclude(c => c.Pelicula)
                .Include(r => r.Cliente)
                .Where(r => r.Cartelera.Pelicula.Genero == genero &&
                            r.Cartelera.Fecha >= fechaInicio &&
                            r.Cartelera.Fecha <= fechaFin)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<object>> ObtenerEstadisticasAsientosPorSala(DateTime fecha)
        {
            return await _context.Salas
                .Select(s => new 
                {
                    SalaId = s.Id,
                    NombreSala = s.Nombre,
                    TotalAsientos = s.Asientos.Count(),
                    AsientosOcupados = s.Carteleras
                        .Where(c => c.Fecha.Date == fecha.Date)
                        .SelectMany(c => c.Reservas)
                        .Select(r => r.AsientoId)
                        .Distinct()
                        .Count(),
                    AsientosDisponibles = s.Asientos.Count() - s.Carteleras
                        .Where(c => c.Fecha.Date == fecha.Date)
                        .SelectMany(c => c.Reservas)
                        .Select(r => r.AsientoId)
                        .Distinct()
                        .Count()
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Reserva>> ObtenerReservasPorCarteleraId(int carteleraId)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Asiento)
                .Where(r => r.CarteleraId == carteleraId)
                .ToListAsync();
        }
    }
}