using Application.DTOs.Reserva;

namespace Application.Interfaces
{
    public interface IServicioReserva
    {
        Task<ReservaDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ReservaDto>> ObtenerTodasAsync();
        Task<ReservaDto> CrearAsync(CrearReservaDto crearReservaDto);
        Task ActualizarAsync(ActualizarReservaDto actualizarReservaDto);
        Task EliminarAsync(int id);
        Task<IEnumerable<ReservaDto>> ObtenerPorClienteAsync(int clienteId);
        Task<IEnumerable<ReservaDto>> ObtenerPorCarteleraAsync(int carteleraId);

        Task<IEnumerable<ReservaDto>> ObtenerReservasDeTerrorPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin);
    }
}