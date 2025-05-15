namespace Application.DTOs.Sala
{
    public class ActualizarSalaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short Numero { get; set; }
        public bool Estado { get; set; }
    }
}