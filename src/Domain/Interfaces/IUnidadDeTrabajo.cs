namespace Domain.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        Task<bool> GuardarCambiosAsync(CancellationToken cancelacion = default);
        Task IniciarTransaccionAsync(CancellationToken cancelacion = default);
        Task ConfirmarTransaccionAsync(CancellationToken cancelacion = default);
        Task RevertirTransaccionAsync(CancellationToken cancelacion = default);
    }
}