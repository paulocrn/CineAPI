using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : Base
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entity);
        Task ActualizarAsync(T entity);
        Task EliminarAsync(T entity);
    }
}