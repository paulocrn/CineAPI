using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IRepositorioCartelera : IRepositorioBase<Cartelera>
    {
        Task<IEnumerable<Reserva>> ObtenerReservasPorGeneroYRangoFechas(GeneroPelicula genero, DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<object>> ObtenerEstadisticasAsientosPorSala(DateTime fecha);
        Task<IEnumerable<Reserva>> ObtenerReservasPorCarteleraId(int carteleraId);
    }
}