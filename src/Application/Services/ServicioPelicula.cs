using Application.DTOs.Pelicula;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServicioPelicula : IServicioPelicula
    {
        private readonly IRepositorioPelicula _repositorio;
        private readonly IMapper _mapeador;

        public ServicioPelicula(IRepositorioPelicula repositorio, IMapper mapeador)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public async Task<PeliculaDto> ObtenerPorIdAsync(int id)
        {
            var pelicula = await _repositorio.ObtenerPorIdAsync(id);
            return _mapeador.Map<PeliculaDto>(pelicula);
        }

        public async Task<IEnumerable<PeliculaDto>> ObtenerTodasAsync()
        {
            var peliculas = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<PeliculaDto>>(peliculas);
        }

        public async Task<PeliculaDto> CrearAsync(CrearPeliculaDto crearPeliculaDto)
        {
            var pelicula = _mapeador.Map<Pelicula>(crearPeliculaDto);
            await _repositorio.AgregarAsync(pelicula);
            return _mapeador.Map<PeliculaDto>(pelicula);
        }

        public async Task ActualizarAsync(ActualizarPeliculaDto actualizarPeliculaDto)
        {
            var pelicula = await _repositorio.ObtenerPorIdAsync(actualizarPeliculaDto.Id);
            _mapeador.Map(actualizarPeliculaDto, pelicula);
            await _repositorio.ActualizarAsync(pelicula);
        }

        public async Task EliminarAsync(int id)
        {
            var pelicula = await _repositorio.ObtenerPorIdAsync(id);
            await _repositorio.EliminarAsync(pelicula);
        }

        public async Task<IEnumerable<PeliculaDto>> ObtenerPorGeneroAsync(GeneroPelicula genero)
        {
            var peliculas = await _repositorio.ObtenerPorGeneroAsync(genero);
            return _mapeador.Map<IEnumerable<PeliculaDto>>(peliculas);
        }

        public async Task<IEnumerable<PeliculaDto>> ObtenerPorNombreAsync(string nombre)
        {
            var peliculas = await _repositorio.ObtenerPorNombreAsync(nombre);
            return _mapeador.Map<IEnumerable<PeliculaDto>>(peliculas);
        }
    }
}