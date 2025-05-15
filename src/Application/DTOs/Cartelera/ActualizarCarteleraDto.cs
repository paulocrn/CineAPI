namespace Application.DTOs.Cartelera
{
    public class ActualizarCarteleraDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Estado { get; set; }
        public int PeliculaId { get; set; }
        public int SalaId { get; set; }
    }
}