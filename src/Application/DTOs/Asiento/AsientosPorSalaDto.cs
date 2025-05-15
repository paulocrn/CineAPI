namespace Application.DTOs.Asiento
{
    public class AsientosPorSalaDto
    {
        public int SalaId { get; set; }
        public string SalaNombre { get; set; } = string.Empty;
        public int AsientosDisponibles { get; set; }
        public int AsientosOcupados { get; set; }
    }
}