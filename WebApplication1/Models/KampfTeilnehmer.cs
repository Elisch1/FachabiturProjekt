namespace WebApplication1.Models
{
    public class KampfTeilnehmer
    {
        public int KampfId { get; set; }
        public Kampf? Kampf { get; set; }

        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int CurrentHp { get; set; }
        public int Initiative { get; set; }
    }
}