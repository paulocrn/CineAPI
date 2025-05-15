using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepositorioAsiento : IRepositorioBase<Asiento> { 
        Task<IEnumerable<Asiento>> ObtenerPorSalaAsync(int salaId);

        Task<(int salaId, string salaNombre, int disponibles, int ocupadas)[]> ObtenerAsientosPorSalaDelDiaAsync();
    }
}