namespace Application.DTOs.Asiento
{
    public class AsientoDto
    {
        public int Id { get; set; }
        public short Numero { get; set; }
        public short Fila { get; set; }
        public bool Estado { get; set; }

        // Relaciones
        public int SalaId { get; set; }
        public string NombreSala { get; set; } = string.Empty;
    }
}