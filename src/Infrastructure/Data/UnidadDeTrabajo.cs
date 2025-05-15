using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly CineDbContext _contexto;
        private IDbContextTransaction? _transaccion;

        public UnidadDeTrabajo(CineDbContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<bool> GuardarCambiosAsync(CancellationToken cancelacion = default)
        {
            return await _contexto.SaveChangesAsync(cancelacion) > 0;
        }

        public async Task IniciarTransaccionAsync(CancellationToken cancelacion = default)
        {
            if (_transaccion != null)
            {
                throw new InvalidOperationException("Ya existe una transacción activa");
            }

            _transaccion = await _contexto.Database.BeginTransactionAsync(cancelacion);
        }

        public async Task ConfirmarTransaccionAsync(CancellationToken cancelacion = default)
        {
            if (_transaccion == null)
            {
                throw new InvalidOperationException("No hay ninguna transacción activa para confirmar");
            }

            try
            {
                await _contexto.SaveChangesAsync(cancelacion);
                await _transaccion.CommitAsync(cancelacion);
            }
            catch
            {
                await RevertirTransaccionAsync(cancelacion);
                throw;
            }
            finally
            {
                _transaccion?.Dispose();
                _transaccion = null;
            }
        }

        public async Task RevertirTransaccionAsync(CancellationToken cancelacion = default)
        {
            if (_transaccion == null)
            {
                throw new InvalidOperationException("No hay ninguna transacción activa para revertir");
            }

            try
            {
                await _transaccion.RollbackAsync(cancelacion);
            }
            finally
            {
                _transaccion?.Dispose();
                _transaccion = null;
            }
        }

        public void Dispose()
        {
            _transaccion?.Dispose();
            _contexto?.Dispose();
        }
    }
}