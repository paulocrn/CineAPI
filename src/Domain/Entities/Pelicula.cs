using Domain.Enums;

namespace Domain.Entities
{
    public class Pelicula : Base
    {
        public string Nombre { get; set; } = string.Empty;
        public GeneroPelicula Genero { get; set; }
        public short EdadMinimaPermitida { get; set; }
        public short DuracionMinutos { get; set; }

        public required ICollection<Cartelera> Carteleras { get; set; }
    }
}