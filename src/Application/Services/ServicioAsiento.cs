using Application.DTOs.Asiento;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServicioAsiento : IServicioAsiento
    {
        private readonly IRepositorioAsiento _repositorio;
        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapeador;

        public ServicioAsiento(
            IRepositorioAsiento repositorio,
            IRepositorioReserva repositorioReserva,
            IUnidadDeTrabajo unidadDeTrabajo,
            IMapper mapeador)
        {
            _repositorio = repositorio;
            _repositorioReserva = repositorioReserva;
            _unidadDeTrabajo = unidadDeTrabajo;
            _mapeador = mapeador;
        }

        public async Task<AsientoDto> ObtenerPorIdAsync(int id)
        {
            var asiento = await _repositorio.ObtenerPorIdAsync(id);
            if (asiento == null || !asiento.Status)
                throw new EntidadNoEncontradaException($"Asiento con ID {id} no encontrado");
            
            return _mapeador.Map<AsientoDto>(asiento);
        }

        public async Task<IEnumerable<AsientoDto>> ObtenerTodosAsync()
        {
            var asientos = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<AsientoDto>>(asientos);
        }

        public async Task<AsientoDto> CrearAsync(CrearAsientoDto crearAsientoDto)
        {
            var asiento = _mapeador.Map<Asiento>(crearAsientoDto);
            await _repositorio.AgregarAsync(asiento);
            return _mapeador.Map<AsientoDto>(asiento);
        }

        public async Task ActualizarAsync(ActualizarAsientoDto actualizarAsientoDto)
        {
            var asiento = await _repositorio.ObtenerPorIdAsync(actualizarAsientoDto.Id);
            if (asiento == null)
                throw new EntidadNoEncontradaException($"Asiento con ID {actualizarAsientoDto.Id} no encontrado");

            _mapeador.Map(actualizarAsientoDto, asiento);
            await _repositorio.ActualizarAsync(asiento);
        }

        public async Task EliminarAsync(int id)
        {
            var asiento = await _repositorio.ObtenerPorIdAsync(id);
            if (asiento == null)
                throw new EntidadNoEncontradaException($"Asiento con ID {id} no encontrado");

            await _repositorio.EliminarAsync(asiento);
        }

        public async Task InhabilitarAsientoYCancelarReserva(int asientoId, int reservaId)
        {
            var asiento = await _repositorio.ObtenerPorIdAsync(asientoId);
            if (asiento == null)
                throw new EntidadNoEncontradaException("Asiento no encontrado");

            var reserva = await _repositorioReserva.ObtenerPorIdAsync(reservaId);
            if (reserva == null)
                throw new EntidadNoEncontradaException("Reserva no encontrada");

            await _unidadDeTrabajo.IniciarTransaccionAsync();

            try
            {
                asiento.Status = false;
                await _repositorio.ActualizarAsync(asiento);
                await _repositorioReserva.EliminarAsync(reserva);
                await _unidadDeTrabajo.ConfirmarTransaccionAsync();
            }
            catch
            {
                await _unidadDeTrabajo.RevertirTransaccionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<AsientoDto>> ObtenerPorSalaAsync(int salaId)
        {
            var sala = await _repositorio.ObtenerPorSalaAsync(salaId);

            if (sala == null)
                throw new EntidadNoEncontradaException($"Sala con ID {sala} no encontrada");

            return _mapeador.Map<IEnumerable<AsientoDto>>(sala);
        }

        public async Task<IEnumerable<AsientosPorSalaDto>> ObtenerAsientosPorSalaDelDiaAsync()
        {
            var datos = await _repositorio.ObtenerAsientosPorSalaDelDiaAsync();

            return datos.Select(d => new AsientosPorSalaDto
            {
                SalaId = d.salaId,
                SalaNombre = d.salaNombre,
                AsientosDisponibles = d.disponibles,
                AsientosOcupados = d.ocupadas
            });
        }
    }
}