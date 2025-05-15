using Application.DTOs.Cartelera;
using Application.DTOs.Queries;
using Application.DTOs.Reserva;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IServicioCartelera
    {
        Task<CarteleraDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<CarteleraDto>> ObtenerTodasAsync();
        Task<CarteleraDto> CrearAsync(CrearCarteleraDto crearCarteleraDto);
        Task ActualizarAsync(ActualizarCarteleraDto actualizarCarteleraDto);
        Task EliminarAsync(int id);
        Task CancelarCarteleraYReservas(int carteleraId);
        Task<IEnumerable<ReservaDto>> ObtenerReservasPorGeneroYRangoFechas(GeneroPelicula genero, DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<EstadisticasAsientosDto>> ObtenerEstadisticasAsientosPorSala(DateTime fecha);
    }
}