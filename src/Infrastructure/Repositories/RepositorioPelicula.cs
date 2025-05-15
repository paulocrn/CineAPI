using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioPelicula : RepositorioBase<Pelicula>, IRepositorioPelicula
    {
        private readonly CineDbContext _contexto;
        public RepositorioPelicula(CineDbContext context) : base(context) {
            _contexto = context;
         }

        public async Task<IEnumerable<Pelicula>> ObtenerPorGeneroAsync(GeneroPelicula genero)
        {
            return await _contexto.Peliculas
                .Where(p => p.Genero == genero && p.Status)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<Pelicula?> ObtenerPorNombreAsync(string nombre)
        {
            return await _contexto.Peliculas
                .FirstOrDefaultAsync(p => p.Nombre == nombre);
        }
    }
}