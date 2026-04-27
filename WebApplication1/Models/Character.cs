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

        // Level and Attributes
        public int Level { get; set; } = 1;
        public int MaxLevel { get; set; } = 10; // Default max level, can be set from DB or config
        public int Strength { get; set; } // Kraft
        public int Precision { get; set; } // Präzision
        public int Agility { get; set; } // Beweglichkeit
        public int Intelligence { get; set; } // Intelligenz
        public int Perception { get; set; } // Wahrnehmung
        public string? CustomAttribute1Name { get; set; }
        public int CustomAttribute1Value { get; set; }
        public string? CustomAttribute2Name { get; set; }
        public int CustomAttribute2Value { get; set; }

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