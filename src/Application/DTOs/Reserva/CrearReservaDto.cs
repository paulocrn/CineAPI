namespace Application.DTOs.Reserva
{
    public class CrearReservaDto
    {
        public DateTime FechaReserva { get; set; }
        public int ClienteId { get; set; }
        public int AsientoId { get; set; }
        public int CarteleraId { get; set; }
    }
}