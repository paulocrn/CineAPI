namespace Application.DTOs.Pelicula
{
    public class CrearPeliculaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public short EdadMinimaPermitida { get; set; }
        public short DuracionMinutos { get; set; }
    }
}