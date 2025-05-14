using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories
{
    public class RepositorioReserva : RepositorioBase<Reserva>, IRepositorioReserva
    {
        public RepositorioReserva(CineDbContext context) : base(context) { }
    }
}