using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories
{
    public class RepositorioSala : RepositorioBase<Sala>, IRepositorioSala
    {
        public RepositorioSala(CineDbContext context) : base(context) { }
    }
}