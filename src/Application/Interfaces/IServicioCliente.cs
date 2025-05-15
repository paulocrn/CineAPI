using Application.DTOs.Cliente;

namespace Application.Interfaces
{
    public interface IServicioCliente
    {
        Task<ClienteDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto> CrearAsync(CrearClienteDto crearClienteDto);
        Task ActualizarAsync(ActualizarClienteDto actualizarClienteDto);
        Task EliminarAsync(int id);
        Task<ClienteDto> ObtenerPorDocumentoAsync(string numeroDocumento);
        Task<IEnumerable<ClienteDto>> BuscarPorNombreAsync(string nombre);
    }
}