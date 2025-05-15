namespace Application.DTOs.Queries
{
    public class EstadisticasAsientosDto
    {
        public int SalaId { get; set; }
        public string NombreSala { get; set; } = string.Empty;
        public int TotalAsientos { get; set; }
        public int AsientosOcupados { get; set; }
        public int AsientosDisponibles { get; set; }
    }
}