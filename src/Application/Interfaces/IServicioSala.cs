using Application.DTOs.Sala;

namespace Application.Interfaces
{
    public interface IServicioSala
    {
        Task<SalaDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<SalaDto>> ObtenerTodasAsync();
        Task<SalaDto> CrearAsync(CrearSalaDto crearSalaDto);
        Task ActualizarAsync(ActualizarSalaDto actualizarSalaDto);
        Task EliminarAsync(int id);
        Task<int> ObtenerCapacidadAsync(int salaId);

        Task<SalaConAsientosDto> ObtenerSalaConAsientosAsync(int salaId);
    }
}