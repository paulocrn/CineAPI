using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories
{
    public class RepositorioPelicula : RepositorioBase<Pelicula>, IRepositorioPelicula
    {
        public RepositorioPelicula(CineDbContext context) : base(context) { }
    }
}