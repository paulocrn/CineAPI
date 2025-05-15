using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepositorioCliente : IRepositorioBase<Cliente> {
        Task<Cliente?> ObtenerPorDocumentoAsync(string numeroDocumento);
        Task<Cliente?> BuscarPorNombreAsync(string nombre);
     }
}