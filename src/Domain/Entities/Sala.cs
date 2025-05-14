namespace Domain.Entities
{
    public class Sala : Base
    {
        public string Nombre { get; set; } = string.Empty;
        public short Numero { get; set; }

        public required ICollection<Cartelera> Carteleras { get; set; }
        public required ICollection<Asiento> Asientos { get; set; }
    }
}