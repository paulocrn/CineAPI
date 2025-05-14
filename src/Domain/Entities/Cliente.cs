namespace Domain.Entities
{
    public class Cliente : Base
    {
        public string NumeroDocumento { get; set; } = String.Empty;
        public string Nombre { get; set; } = String.Empty;
        public string Apellido { get; set; } = String.Empty;
        public short Edad { get; set; }
        public string Telefono { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        public required ICollection<Reserva> Reservas { get; set; }
    }
}