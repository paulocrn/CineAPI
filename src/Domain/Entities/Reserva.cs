namespace Domain.Entities
{
    public class Reserva : Base
    {
        public DateTime FechaReserva { get; set; }

        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }

        public int AsientoId { get; set; }
        public required Asiento Asiento { get; set; }

        public int CarteleraId { get; set; }
        public required Cartelera Cartelera { get; set; }
    }
}