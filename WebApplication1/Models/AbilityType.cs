namespace WebApplication1.Models
{
    public class AbilityType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Ability> Abilities { get; set; } = new();
    }
}