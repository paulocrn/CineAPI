using Application.DTOs.Asiento;
using Application.DTOs.Cartelera;
using Application.DTOs.Cliente;
using Application.DTOs.Pelicula;
using Application.DTOs.Queries;
using Application.DTOs.Reserva;
using Application.DTOs.Sala;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mappings
{
    public class PerfilAutoMapper : Profile
    {
        public PerfilAutoMapper()
        {
            // Cartelera
            CreateMap<Cartelera, CarteleraDto>()
                .ForMember(dest => dest.NombrePelicula, opt => opt.MapFrom(src => src.Pelicula.Nombre))
                .ForMember(dest => dest.GeneroPelicula, opt => opt.MapFrom(src => src.Pelicula.Genero.ToString()))
                .ForMember(dest => dest.NombreSala, opt => opt.MapFrom(src => src.Sala.Nombre))
                .ForMember(dest => dest.NumeroSala, opt => opt.MapFrom(src => src.Sala.Numero));
                
            CreateMap<CrearCarteleraDto, Cartelera>();
            CreateMap<ActualizarCarteleraDto, Cartelera>();

            // Asiento
            CreateMap<Asiento, AsientoDto>()
                .ForMember(dest => dest.NombreSala, opt => opt.MapFrom(src => src.Sala.Nombre));
                
            CreateMap<CrearAsientoDto, Asiento>();
            CreateMap<ActualizarAsientoDto, Asiento>();
            CreateMap<AsientosPorSalaDto, Asiento>();

            // Sala
            CreateMap<Sala, SalaDto>()
                .ForMember(dest => dest.Capacidad, opt => opt.MapFrom(src => src.Asientos.Count()));

            CreateMap<Sala, SalaConAsientosDto>()
                .ForMember(dest => dest.Asientos, opt => opt.MapFrom(src => src.Asientos));
                
            CreateMap<CrearSalaDto, Sala>();
            CreateMap<ActualizarSalaDto, Sala>();

            // Reserva
            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => $"{src.Cliente.Nombre} {src.Cliente.Apellido}"))
                .ForMember(dest => dest.DocumentoCliente, opt => opt.MapFrom(src => src.Cliente.NumeroDocumento))
                .ForMember(dest => dest.DescripcionAsiento, opt => opt.MapFrom(src => $"Fila {src.Asiento.Fila} - Asiento {src.Asiento.Numero}"))
                .ForMember(dest => dest.DetalleCartelera, opt => opt.MapFrom(src => 
                    $"{src.Cartelera.Pelicula.Nombre} - {src.Cartelera.Fecha:dd/MM/yyyy} {src.Cartelera.HoraInicio:hh\\:mm}"));
                
            CreateMap<CrearReservaDto, Reserva>();
            CreateMap<ActualizarReservaDto, Reserva>();

            // Película
            CreateMap<Pelicula, PeliculaDto>()
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero.ToString()));
                
            CreateMap<CrearPeliculaDto, Pelicula>()
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => Enum.Parse<GeneroPelicula>(src.Genero)));
                
            CreateMap<ActualizarPeliculaDto, Pelicula>()
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => Enum.Parse<GeneroPelicula>(src.Genero)));

            // Cliente
            CreateMap<Cliente, ClienteDto>();
            CreateMap<CrearClienteDto, Cliente>();
            CreateMap<ActualizarClienteDto, Cliente>();

            // Mapeos especializados
            CreateMap<Cartelera, InformeCarteleraDto>()
                .ForMember(dest => dest.Pelicula, opt => opt.MapFrom(src => src.Pelicula.Nombre))
                .ForMember(dest => dest.Sala, opt => opt.MapFrom(src => $"{src.Sala.Nombre} ({src.Sala.Numero})"))
                .ForMember(dest => dest.TotalReservas, opt => opt.MapFrom(src => src.Reservas.Count))
                .ForMember(dest => dest.IngresosEstimados, opt => opt.MapFrom(src => src.Reservas.Count * 10)); // Precio ficticio
        }
    }
}