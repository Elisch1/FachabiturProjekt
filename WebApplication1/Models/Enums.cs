namespace WebApplication1.Models
{
    public enum CharacterType
    {
        Player,
        NPC,
        Enemy
    }

    public enum WeaponType
    {
        Melee,
        Range,
        Magic
    }

    public enum ArmorSlot
    {
        Head,
        Chest,
        Legs,
        // add others as needed
    }

    public enum KampfStatus
    {
        Aktiv,
        Beendet
    }
}