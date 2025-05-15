namespace Application.DTOs.Queries
{
    public class InformeCarteleraDto
    {
        public int CarteleraId { get; set; }
        public string Pelicula { get; set; } = string.Empty;
        public string Sala { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int TotalReservas { get; set; }
        public decimal IngresosEstimados { get; set; }
    }
}