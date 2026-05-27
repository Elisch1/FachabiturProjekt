using System;

namespace WebApplication1.Models
{
    public class KampfLog
    {
        public int Id { get; set; }
        public int KampfId { get; set; }
        public Kampf? Kampf { get; set; }

        public int? AngreiferId { get; set; }
        public Character? Angreifer { get; set; }

        public int? VerteidigerId { get; set; }
        public Character? Verteidiger { get; set; }

        public int Rolled { get; set; }
        public int Damage { get; set; }
        public string Nachricht { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}