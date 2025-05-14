namespace Domain.Entities
{
    public class Asiento : Base
    {
        public short Numero { get; set; }
        public short Fila { get; set; }

        public int SalaId { get; set; }
        public required Sala Sala { get; set; }

        public required ICollection<Reserva> Reservas { get; set; }
    }
}