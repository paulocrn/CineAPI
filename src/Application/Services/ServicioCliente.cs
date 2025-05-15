using Application.DTOs.Cliente;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ServicioCliente : IServicioCliente
    {
        private readonly IRepositorioCliente _repositorio;
        private readonly IMapper _mapeador;

        public ServicioCliente(IRepositorioCliente repositorio, IMapper mapeador)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public async Task<ClienteDto> ObtenerPorIdAsync(int id)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(id);
            return _mapeador.Map<ClienteDto>(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _repositorio.ObtenerTodosAsync();
            return _mapeador.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> CrearAsync(CrearClienteDto crearClienteDto)
        {
            var cliente = _mapeador.Map<Cliente>(crearClienteDto);
            await _repositorio.AgregarAsync(cliente);
            return _mapeador.Map<ClienteDto>(cliente);
        }

        public async Task ActualizarAsync(ActualizarClienteDto actualizarClienteDto)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(actualizarClienteDto.Id);
            _mapeador.Map(actualizarClienteDto, cliente);
            await _repositorio.ActualizarAsync(cliente);
        }

        public async Task EliminarAsync(int id)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(id);
            await _repositorio.EliminarAsync(cliente);
        }

        public async Task<ClienteDto> ObtenerPorDocumentoAsync(string numeroDocumento)
        {
            var cliente = await _repositorio.ObtenerPorDocumentoAsync(numeroDocumento);
            return _mapeador.Map<ClienteDto>(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> BuscarPorNombreAsync(string nombre)
        {
            var clientes = await _repositorio.BuscarPorNombreAsync(nombre);
            return _mapeador.Map<IEnumerable<ClienteDto>>(clientes);
        }
    }
}