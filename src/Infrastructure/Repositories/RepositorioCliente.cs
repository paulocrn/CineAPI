using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories
{
    public class RepositorioCliente : RepositorioBase<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(CineDbContext context) : base(context) { }
    }
}