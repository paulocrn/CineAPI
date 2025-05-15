using Application.DTOs.Sala;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServicioSala : IServicioSala
    {
        private readonly IRepositorioSala _repositorio;
        private readonly IMapper _mapeador;

        public ServicioSala(IRepositorioSala repositorio, IMapper mapeador)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public async Task<SalaDto> ObtenerPorIdAsync(int id)
        {
            var sala = await _repositorio.ObtenerPorIdAsync(id);
            return _mapeador.Map<SalaDto>(sala);
        }

        public async Task<IEnumerable<SalaDto>> ObtenerTodasAsync()
        {
            var salas = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<SalaDto>>(salas);
        }

        public async Task<SalaDto> CrearAsync(CrearSalaDto crearSalaDto)
        {
            var sala = _mapeador.Map<Sala>(crearSalaDto);
            await _repositorio.AgregarAsync(sala);
            return _mapeador.Map<SalaDto>(sala);
        }

        public async Task ActualizarAsync(ActualizarSalaDto actualizarSalaDto)
        {
            var sala = await _repositorio.ObtenerPorIdAsync(actualizarSalaDto.Id);
            _mapeador.Map(actualizarSalaDto, sala);
            await _repositorio.ActualizarAsync(sala);
        }

        public async Task EliminarAsync(int id)
        {
            var sala = await _repositorio.ObtenerPorIdAsync(id);
            await _repositorio.EliminarAsync(sala);
        }

        public async Task<int> ObtenerCapacidadAsync(int salaId)
        {
            var sala = await _repositorio.ObtenerConAsientosAsync(salaId);
            return sala?.Asientos.Count ?? 0;
        }

        public async Task<SalaConAsientosDto> ObtenerSalaConAsientosAsync(int salaId)
        {
            var sala = await _repositorio.ObtenerConAsientosAsync(salaId);
            return _mapeador.Map<SalaConAsientosDto>(sala);
        }
    }
}