using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioCliente : RepositorioBase<Cliente>, IRepositorioCliente
    {
        private readonly CineDbContext _contexto;
        public RepositorioCliente(CineDbContext context) : base(context) {
            _contexto = context;
         }

        public async Task<Cliente?> BuscarPorNombreAsync(string nombre)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Nombre == nombre);
        }

        public async Task<Cliente?> ObtenerPorDocumentoAsync(string numeroDocumento)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.NumeroDocumento == numeroDocumento);
        }
    }
}