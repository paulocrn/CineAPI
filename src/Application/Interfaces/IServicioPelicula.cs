using Application.DTOs.Pelicula;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IServicioPelicula
    {
        Task<PeliculaDto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<PeliculaDto>> ObtenerTodasAsync();
        Task<PeliculaDto> CrearAsync(CrearPeliculaDto crearPeliculaDto);
        Task ActualizarAsync(ActualizarPeliculaDto actualizarPeliculaDto);
        Task EliminarAsync(int id);
        Task<IEnumerable<PeliculaDto>> ObtenerPorGeneroAsync(GeneroPelicula genero);
        Task<IEnumerable<PeliculaDto>> ObtenerPorNombreAsync(string nombre);
    }
}