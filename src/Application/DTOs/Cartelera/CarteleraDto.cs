using Application.DTOs.Pelicula;
using Domain.Entities;

namespace Application.DTOs.Cartelera
{
    public class CarteleraDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Estado { get; set; }

        // Relaciones
        public int PeliculaId { get; set; }
        public string NombrePelicula { get; set; } = string.Empty;
        public string GeneroPelicula { get; set; } = string.Empty;

        public int SalaId { get; set; }
        public string NombreSala { get; set; } = string.Empty;
        public int NumeroSala { get; set; }

    }
}