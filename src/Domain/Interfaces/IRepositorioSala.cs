using Domain.Entities;

namespace Domain.Interfaces
{
    public  interface IRepositorioSala : IRepositorioBase<Sala> {
        Task<Sala?> ObtenerConAsientosAsync(int? salaId);
     }
}