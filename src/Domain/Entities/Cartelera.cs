namespace Domain.Entities
{
    public class Cartelera : Base
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public int PeliculaId { get; set; }
        public required Pelicula Pelicula { get; set; }

        public int SalaId { get; set; }
        public required Sala Sala { get; set; }

        public required ICollection<Reserva> Reservas { get; set; }
    }
}