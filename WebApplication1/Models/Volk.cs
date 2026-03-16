namespace WebApplication1.Models
{
    public class Volk
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Character> Characters { get; set; } = new();
    }
}