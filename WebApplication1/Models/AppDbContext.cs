using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // original example
        //public DbSet<Product> Products => Set<Product>();

        // pen & paper entities
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<AttributeDefinition> AttributeDefinitions => Set<AttributeDefinition>();
        public DbSet<AbilityType> AbilityTypes => Set<AbilityType>();
        public DbSet<Ability> Abilities => Set<Ability>();
        public DbSet<CharacterAbility> CharacterAbilities => Set<CharacterAbility>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<ItemType> ItemTypes => Set<ItemType>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<WeaponStats> WeaponStats => Set<WeaponStats>();
        public DbSet<ArmorStats> ArmorStats => Set<ArmorStats>();
        public DbSet<ConsumableStats> ConsumableStats => Set<ConsumableStats>();
        public DbSet<Volk> Völker => Set<Volk>();
        public DbSet<Religion> Religionen => Set<Religion>();
        public DbSet<Beruf> Berufe => Set<Beruf>();
        public DbSet<Race> Races => Set<Race>();
        public DbSet<Welt> Welten => Set<Welt>();
        public DbSet<Ort> Orte => Set<Ort>();
        public DbSet<Fraktion> Fraktionen => Set<Fraktion>();
        public DbSet<Kampf> Kämpfe => Set<Kampf>();
        public DbSet<KampfTeilnehmer> KampfTeilnehmer => Set<KampfTeilnehmer>();
        public DbSet<KampfLog> KampfLogs => Set<KampfLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // character <-> inventory one-to-one
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Inventory)
                .WithOne()
                .HasForeignKey<Character>(c => c.InventoryId);

            // character ability many-to-many with extra field
            modelBuilder.Entity<CharacterAbility>()
                .HasKey(ca => new { ca.CharacterId, ca.AbilityId });
            modelBuilder.Entity<CharacterAbility>()
                .HasOne(ca => ca.Character)
                .WithMany(c => c.Abilities)
                .HasForeignKey(ca => ca.CharacterId);
            modelBuilder.Entity<CharacterAbility>()
                .HasOne(ca => ca.Ability)
                .WithMany(a => a.CharacterAbilities)
                .HasForeignKey(ca => ca.AbilityId);

            // inventory items composite key
            modelBuilder.Entity<InventoryItem>()
                .HasKey(ii => new { ii.InventoryId, ii.ItemId });
            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Inventory)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InventoryId);
            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Item)
                .WithMany(i => i.InventoryItems)
                .HasForeignKey(ii => ii.ItemId);

            // combat participants composite key
            modelBuilder.Entity<KampfTeilnehmer>()
                .HasKey(kt => new { kt.KampfId, kt.CharacterId });
            modelBuilder.Entity<KampfTeilnehmer>()
                .HasOne(kt => kt.Kampf)
                .WithMany(k => k.Teilnehmer)
                .HasForeignKey(kt => kt.KampfId);
            modelBuilder.Entity<KampfTeilnehmer>()
                .HasOne(kt => kt.Character)
                .WithMany(c => c.KampfTeilnehmer)
                .HasForeignKey(kt => kt.CharacterId);

            // item stats one-to-one
            modelBuilder.Entity<WeaponStats>()
                .HasKey(ws => ws.ItemId);
            modelBuilder.Entity<WeaponStats>()
                .HasOne(ws => ws.Item)
                .WithOne(i => i.WeaponStats)
                .HasForeignKey<WeaponStats>(ws => ws.ItemId);

            modelBuilder.Entity<ArmorStats>()
                .HasKey(a => a.ItemId);
            modelBuilder.Entity<ArmorStats>()
                .HasOne(a => a.Item)
                .WithOne(i => i.ArmorStats)
                .HasForeignKey<ArmorStats>(a => a.ItemId);

            modelBuilder.Entity<ConsumableStats>()
                .HasKey(c => c.ItemId);
            modelBuilder.Entity<ConsumableStats>()
                .HasOne(c => c.Item)
                .WithOne(i => i.ConsumableStats)
                .HasForeignKey<ConsumableStats>(c => c.ItemId);

            // world relations
            modelBuilder.Entity<Ort>()
                .HasOne(o => o.Welt)
                .WithMany(w => w.Orte)
                .HasForeignKey(o => o.WeltId);
            modelBuilder.Entity<Fraktion>()
                .HasOne(f => f.Welt)
                .WithMany(w => w.Fraktionen)
                .HasForeignKey(f => f.WeltId);
        }
    }
}