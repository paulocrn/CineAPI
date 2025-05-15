using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioSala : RepositorioBase<Sala>, IRepositorioSala
    {
        private readonly CineDbContext _contexto;
        public RepositorioSala(CineDbContext context) : base(context) {
            _contexto = context;
        }

        public async Task<Sala?> ObtenerConAsientosAsync(int? salaId)
        {
            return await _contexto.Salas
                .Include(s => s.Asientos)
                .FirstOrDefaultAsync(s => s.Id == salaId);
        }
    }
}