namespace WebApplication1.Models
{
    public class ConsumableStats
    {
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public string? Effect { get; set; }
        public int Duration { get; set; }
        public int Cooldown { get; set; }
    }
}