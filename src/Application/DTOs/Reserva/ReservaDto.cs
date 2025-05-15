namespace Application.DTOs.Reserva
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public bool Estado { get; set; }

        // Relaciones
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string DocumentoCliente { get; set; } = string.Empty;

        public int AsientoId { get; set; }
        public string DescripcionAsiento { get; set; } = string.Empty;

        public int CarteleraId { get; set; }
        public string DetalleCartelera { get; set; } = string.Empty;
    }
}