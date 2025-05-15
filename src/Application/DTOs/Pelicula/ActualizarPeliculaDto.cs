namespace Application.DTOs.Pelicula
{
    public class ActualizarPeliculaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public short EdadMinimaPermitida { get; set; }
        public short DuracionMinutos { get; set; }
        public bool Estado { get; set; }
    }
}