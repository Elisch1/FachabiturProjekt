namespace WebApplication1.Models
{
    public class Religion
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Character> Characters { get; set; } = new();
    }
}