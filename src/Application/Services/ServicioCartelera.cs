using Application.DTOs.Cartelera;
using Application.DTOs.Queries;
using Application.DTOs.Reserva;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServicioCartelera : IServicioCartelera
    {
        private readonly IRepositorioCartelera _repositorio;
        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IRepositorioAsiento _repositorioAsiento;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapeador;
        private readonly ILogger<ServicioCartelera> _logger;

        public ServicioCartelera(
            IRepositorioCartelera repositorio,
            IRepositorioReserva repositorioReserva,
            IRepositorioAsiento repositorioAsiento,
            IUnidadDeTrabajo unidadDeTrabajo,
            IMapper mapeador,
            ILogger<ServicioCartelera> logger)
        {
            _repositorio = repositorio;
            _repositorioReserva = repositorioReserva;
            _repositorioAsiento = repositorioAsiento;
            _unidadDeTrabajo = unidadDeTrabajo;
            _mapeador = mapeador;
            _logger = logger;
        }

        public async Task<CarteleraDto> ObtenerPorIdAsync(int id)
        {
            var cartelera = await _repositorio.ObtenerPorIdAsync(id);
            return _mapeador.Map<CarteleraDto>(cartelera);
        }

        public async Task<IEnumerable<CarteleraDto>> ObtenerTodasAsync()
        {
            var carteleras = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<CarteleraDto>>(carteleras);
        }

        public async Task<CarteleraDto> CrearAsync(CrearCarteleraDto crearCarteleraDto)
        {
            var cartelera = _mapeador.Map<Cartelera>(crearCarteleraDto);
            await _repositorio.AgregarAsync(cartelera);
            return _mapeador.Map<CarteleraDto>(cartelera);
        }

        public async Task ActualizarAsync(ActualizarCarteleraDto actualizarCarteleraDto)
        {
            var cartelera = await _repositorio.ObtenerPorIdAsync(actualizarCarteleraDto.Id);
            _mapeador.Map(actualizarCarteleraDto, cartelera);
            await _repositorio.ActualizarAsync(cartelera);
        }

        public async Task EliminarAsync(int id)
        {
            var cartelera = await _repositorio.ObtenerPorIdAsync(id);
            await _repositorio.EliminarAsync(cartelera);
        }

        public async Task CancelarCarteleraYReservas(int carteleraId)
        {
            var cartelera = await _repositorio.ObtenerPorIdAsync(carteleraId);
            if (cartelera == null)
                throw new EntidadNoEncontradaException("Cartelera no encontrada");

            if (DateTime.Now.Date > cartelera.Fecha.Date)
                throw new ReglaDeNegocioException("No se puede cancelar funciones pasadas");

            await _unidadDeTrabajo.IniciarTransaccionAsync();

            try
            {
                cartelera.Status = false;
                await _repositorio.ActualizarAsync(cartelera);

                var reservas = await _repositorioReserva.ObtenerPorCarteleraAsync(carteleraId);
                var clientesAfectados = new List<Cliente>();

                foreach (var reserva in reservas)
                {
                    var asiento = await _repositorioAsiento.ObtenerPorIdAsync(reserva.AsientoId);
                    if (asiento != null)
                    {
                        asiento.Status = true;
                        await _repositorioAsiento.ActualizarAsync(asiento);
                    }

                    await _repositorioReserva.EliminarAsync(reserva);
                    clientesAfectados.Add(reserva.Cliente);
                }

                await _unidadDeTrabajo.ConfirmarTransaccionAsync();

                _logger.LogInformation("Clientes afectados: {Clientes}", 
                    string.Join(", ", clientesAfectados.Select(c => $"{c.Nombre} {c.Apellido}")));
            }
            catch
            {
                await _unidadDeTrabajo.RevertirTransaccionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerReservasPorGeneroYRangoFechas(
            GeneroPelicula genero, DateTime fechaInicio, DateTime fechaFin)
        {
            var reservas = await _repositorio.ObtenerReservasPorGeneroYRangoFechas(genero, fechaInicio, fechaFin);
            return _mapeador.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<IEnumerable<EstadisticasAsientosDto>> ObtenerEstadisticasAsientosPorSala(DateTime fecha)
        {
            var estadisticas = await _repositorio.ObtenerEstadisticasAsientosPorSala(fecha);
            return _mapeador.Map<IEnumerable<EstadisticasAsientosDto>>(estadisticas);
        }
    }
}