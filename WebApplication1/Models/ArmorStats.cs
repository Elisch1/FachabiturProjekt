namespace WebApplication1.Models
{
    public class ArmorStats
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int Defense { get; set; }
        public int Durability { get; set; }
        public ArmorSlot Slot { get; set; }
    }
}