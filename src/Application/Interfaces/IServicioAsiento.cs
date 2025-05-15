using Application.DTOs.Asiento;
using Application.DTOs.Sala;

namespace Application.Interfaces
{
    public interface IServicioAsiento
    {
        Task<AsientoDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<AsientoDto>> ObtenerTodosAsync();
        Task<AsientoDto> CrearAsync(CrearAsientoDto crearAsientoDto);
        Task ActualizarAsync(ActualizarAsientoDto actualizarAsientoDto);
        Task EliminarAsync(int id);
        Task InhabilitarAsientoYCancelarReserva(int asientoId, int reservaId);
        Task<IEnumerable<AsientoDto>> ObtenerPorSalaAsync(int salaId);

        Task<IEnumerable<AsientosPorSalaDto>> ObtenerAsientosPorSalaDelDiaAsync();
    }
}