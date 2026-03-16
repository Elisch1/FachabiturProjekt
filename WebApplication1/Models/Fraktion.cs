namespace WebApplication1.Models
{
    public class Fraktion
    {
        public int Id { get; set; }
        public int WeltId { get; set; }
        public Welt? Welt { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}