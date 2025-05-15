using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IRepositorioPelicula : IRepositorioBase<Pelicula> {
        Task<IEnumerable<Pelicula>> ObtenerPorGeneroAsync(GeneroPelicula genero);

        Task<Pelicula?> ObtenerPorNombreAsync(string nombre);
     }
}