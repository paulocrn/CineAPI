namespace Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        public bool Status { get; set; } = true;
    }
}