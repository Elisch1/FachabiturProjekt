namespace WebApplication1.Models
{
    public class Welt
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Ort> Orte { get; set; } = new();
        public List<Fraktion> Fraktionen { get; set; } = new();
    }
}