namespace WebApplication1.Models
{
    public class WeaponStats
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int Damage { get; set; }
        public int Range { get; set; }
        public int Durability { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}