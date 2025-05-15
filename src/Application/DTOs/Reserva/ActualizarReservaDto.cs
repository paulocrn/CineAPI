namespace Application.DTOs.Reserva
{
    public class ActualizarReservaDto
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
        public int AsientoId { get; set; }
        public int CarteleraId { get; set; }
    }
}