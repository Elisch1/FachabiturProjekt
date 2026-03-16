using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Capacity { get; set; }

        public List<InventoryItem> Items { get; set; } = new();
    }
}