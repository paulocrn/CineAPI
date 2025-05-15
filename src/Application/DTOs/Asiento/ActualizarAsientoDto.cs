namespace Application.DTOs.Asiento
{
    public class ActualizarAsientoDto
    {
        public int Id { get; set; }
        public short Numero { get; set; }
        public short Fila { get; set; }
        public bool Estado { get; set; }
        public int SalaId { get; set; }
    }
}