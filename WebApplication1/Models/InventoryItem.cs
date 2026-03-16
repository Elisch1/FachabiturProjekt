namespace WebApplication1.Models
{
    public class InventoryItem
    {
        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int Quantity { get; set; }
    }
}