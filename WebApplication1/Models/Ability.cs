namespace WebApplication1.Models
{
    public class Ability
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int MaxLevel { get; set; }

        public int AbilityTypeId { get; set; }
        public AbilityType? AbilityType { get; set; }

        public List<CharacterAbility> CharacterAbilities { get; set; } = new();
    }
}