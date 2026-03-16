using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Character
    {
        public int Id { get; set; }
        public CharacterType Type { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        // foreign keys
        public int? VolkId { get; set; }
        public Volk? Volk { get; set; }

        public int? ReligionId { get; set; }
        public Religion? Religion { get; set; }

        public int? BerufId { get; set; }
        public Beruf? Beruf { get; set; }

        public int? RaceId { get; set; }
        public Race? Race { get; set; }

        public int? InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
        public string? ProfileImagePath { get; set; }

        // navigation collections
        public List<CharacterAbility> Abilities { get; set; } = new();
        public List<KampfTeilnehmer> KampfTeilnehmer { get; set; } = new();
    }
}