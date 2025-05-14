using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories
{
    public class RepositorioAsiento : RepositorioBase<Asiento>, IRepositorioAsiento
    {
        public RepositorioAsiento(CineDbContext context) : base(context) { }
    }
}