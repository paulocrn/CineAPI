using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.Asiento;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioAsiento : RepositorioBase<Asiento>, IRepositorioAsiento
    {
        private readonly CineDbContext _contexto;
        public RepositorioAsiento(CineDbContext context) : base(context) { 
            _contexto = context;
        }

        public async Task<IEnumerable<Asiento>> ObtenerPorSalaAsync(int salaId)
        {
            return await _contexto.Asientos
                .Include(a => a.Sala)
                .Where(a => a.SalaId == salaId)
                .OrderBy(a => a.Fila)
                .ThenBy(a => a.Numero)
                .ToListAsync();
        }

        public async Task<(int salaId, string salaNombre, int disponibles, int ocupadas)[]> ObtenerAsientosPorSalaDelDiaAsync()
        {
            var hoy = DateTime.Today;

            var resultados = await _context.Carteleras
                .Where(c => c.Fecha.Date == hoy)
                .Select(c => c.Sala)
                .Distinct()
                .Select(sala => new
                {
                    sala.Id,
                    sala.Nombre,
                    Disponibles = sala.Asientos.Count(a => a.Status),
                    Ocupadas = sala.Asientos.Count(a => !a.Status)
                })
                .ToArrayAsync();

            return resultados
                .Select(r => (r.Id, r.Nombre, r.Disponibles, r.Ocupadas))
                .ToArray();
        }
    }
}