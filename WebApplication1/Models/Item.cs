namespace WebApplication1.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Weight { get; set; }

        public int ItemTypeId { get; set; }
        public ItemType? ItemType { get; set; }

        // navigation to specific stats
        public WeaponStats? WeaponStats { get; set; }
        public ArmorStats? ArmorStats { get; set; }
        public ConsumableStats? ConsumableStats { get; set; }

        public List<InventoryItem> InventoryItems { get; set; } = new();
    }
}