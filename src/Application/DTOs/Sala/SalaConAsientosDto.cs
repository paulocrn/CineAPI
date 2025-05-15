using Application.DTOs.Asiento;

namespace Application.DTOs.Sala
{
    public class SalaConAsientosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short Numero { get; set; }
        public bool Estado { get; set; }
        public List<AsientoDto> Asientos { get; set; } = new();
    }
}