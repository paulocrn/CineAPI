namespace Application.DTOs.Cartelera
{
    public class CrearCarteleraDto
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int PeliculaId { get; set; }
        public int SalaId { get; set; }
    }
}