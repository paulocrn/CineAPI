using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepositorioReserva : IRepositorioBase<Reserva> {
        Task<Reserva?> ObtenerPorClienteAsync(int clienteId);

        Task<IEnumerable<Reserva>> ObtenerPorCarteleraAsync(int carteleraId);

        Task<IEnumerable<Reserva>> ObtenerReservasDeTerrorPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin);
     }
}