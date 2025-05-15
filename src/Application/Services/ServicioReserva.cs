using Application.DTOs.Reserva;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServicioReserva : IServicioReserva
    {
        private readonly IRepositorioReserva _repositorio;
        private readonly IMapper _mapeador;

        public ServicioReserva(IRepositorioReserva repositorio, IMapper mapeador)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public async Task<ReservaDto> ObtenerPorIdAsync(int id)
        {
            var reserva = await _repositorio.ObtenerPorIdAsync(id);
            return _mapeador.Map<ReservaDto>(reserva);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerTodasAsync()
        {
            var reservas = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<ReservaDto> CrearAsync(CrearReservaDto crearReservaDto)
        {
            var reserva = _mapeador.Map<Reserva>(crearReservaDto);
            await _repositorio.AgregarAsync(reserva);
            return _mapeador.Map<ReservaDto>(reserva);
        }

        public async Task ActualizarAsync(ActualizarReservaDto actualizarReservaDto)
        {
            var reserva = await _repositorio.ObtenerPorIdAsync(actualizarReservaDto.Id);
            _mapeador.Map(actualizarReservaDto, reserva);
            await _repositorio.ActualizarAsync(reserva);
        }

        public async Task EliminarAsync(int id)
        {
            var reserva = await _repositorio.ObtenerPorIdAsync(id);
            await _repositorio.EliminarAsync(reserva);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerPorClienteAsync(int clienteId)
        {
            var reservas = await _repositorio.ObtenerPorClienteAsync(clienteId);
            return _mapeador.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerPorCarteleraAsync(int carteleraId)
        {
            var reservas = await _repositorio.ObtenerPorCarteleraAsync(carteleraId);
            return _mapeador.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<IEnumerable<ReservaDto>> ObtenerReservasDeTerrorPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin){
            var reservas = await _repositorio.ObtenerReservasDeTerrorPorRangoDeFechas(fechaInicio, fechaFin);
            return _mapeador.Map<IEnumerable<ReservaDto>>(reservas);
        }
    }
}