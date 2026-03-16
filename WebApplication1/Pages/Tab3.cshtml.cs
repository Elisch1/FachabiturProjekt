using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class Tab3Model : PageModel
    {
        private readonly AppDbContext _db;

        public List<Character> Characters { get; set; } = new();
        public List<Item> AllItems { get; set; } = new();

        public Character? SelectedCharacter { get; set; }
        public Inventory? SelectedInventory { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCharacterId { get; set; }

        [BindProperty]
        public int SelectedItemId { get; set; }

        [BindProperty]
        public int Quantity { get; set; } = 1;

        public Tab3Model(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            LoadCharacters();
            LoadItems();

            if (SelectedCharacterId.HasValue)
            {
                LoadSelectedCharacter(SelectedCharacterId.Value);
            }
        }

        public IActionResult OnPostSelectCharacter()
        {
            LoadCharacters();
            LoadItems();

            if (SelectedCharacterId.HasValue)
            {
                LoadSelectedCharacter(SelectedCharacterId.Value);
            }

            return Page();
        }

        public IActionResult OnPostAddItem()
        {
            LoadCharacters();
            LoadItems();

            if (!SelectedCharacterId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Bitte wähle zuerst einen Charakter aus.");
                return Page();
            }

            if (Quantity <= 0)
            {
                ModelState.AddModelError(nameof(Quantity), "Die Menge muss mindestens 1 sein.");
            }

            var characterId = SelectedCharacterId.Value;

            if (!ModelState.IsValid)
            {
                LoadSelectedCharacter(characterId);
                return Page();
            }

            if (SelectedItemId <= 0)
            {
                ModelState.AddModelError(nameof(SelectedItemId), "Bitte wähle einen gültigen Gegenstand aus.");
                LoadSelectedCharacter(characterId);
                return Page();
            }

            var item = _db.Items.Find(SelectedItemId);
            if (item == null)
            {
                ModelState.AddModelError(nameof(SelectedItemId), "Der ausgewählte Gegenstand konnte nicht gefunden werden.");
                LoadSelectedCharacter(characterId);
                return Page();
            }

#pragma warning disable CS8602
            var character = _db.Characters
                .Include(c => c.Inventory)
                .ThenInclude(i => i!.Items)
                .ThenInclude(ii => ii!.Item)
                .ThenInclude(i => i!.ItemType)
                .FirstOrDefault(c => c.Id == characterId);
#pragma warning restore CS8602

            if (character == null)
            {
                ModelState.AddModelError(string.Empty, "Charakter nicht gefunden.");
                return Page();
            }

            if (character.Inventory == null)
            {
                var newInventory = new Inventory { Capacity = 50 };
                _db.Inventories.Add(newInventory);
                _db.SaveChanges();

                character.Inventory = newInventory;
                character.InventoryId = newInventory.Id;
                _db.Characters.Update(character);
                _db.SaveChanges();
            }

            // Ensure inventory is loaded after possible creation
            var inventoryId = character.InventoryId;
            if (!inventoryId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Inventar konnte nicht geladen werden.");
                return Page();
            }

#pragma warning disable CS8602
            var inventory = _db.Inventories
                .Include(i => i.Items)
                .ThenInclude(ii => ii!.Item)
                .ThenInclude(i => i!.ItemType)
                .FirstOrDefault(i => i.Id == inventoryId.Value);
#pragma warning restore CS8602

            if (inventory == null)
            {
                ModelState.AddModelError(string.Empty, "Inventar konnte nicht geladen werden.");
                return Page();
            }

            var existing = _db.InventoryItems
                .FirstOrDefault(ii => ii.InventoryId == inventory.Id && ii.ItemId == SelectedItemId);

            if (existing != null)
            {
                existing.Quantity += Quantity;
                _db.InventoryItems.Update(existing);
            }
            else
            {
                _db.InventoryItems.Add(new InventoryItem
                {
                    InventoryId = inventory.Id,
                    ItemId = SelectedItemId,
                    Quantity = Quantity
                });
            }

            _db.SaveChanges();

            return RedirectToPage(new { SelectedCharacterId });
        }

        public IActionResult OnPostUpdateQuantity(int itemId, int quantity)
        {
            if (!SelectedCharacterId.HasValue)
            {
                return RedirectToPage();
            }

            var characterId = SelectedCharacterId.Value;
            var character = _db.Characters
                .Include(c => c.Inventory)
                .ThenInclude(i => i!.Items)
                .FirstOrDefault(c => c.Id == characterId);

            if (character?.Inventory == null)
                return RedirectToPage(new { SelectedCharacterId });

            var inventoryId = character.InventoryId;
            if (!inventoryId.HasValue)
                return RedirectToPage(new { SelectedCharacterId });

            var inventoryItem = _db.InventoryItems
                .FirstOrDefault(ii => ii.InventoryId == inventoryId.Value && ii.ItemId == itemId);

            if (inventoryItem == null)
                return RedirectToPage(new { SelectedCharacterId });

            if (quantity <= 0)
            {
                _db.InventoryItems.Remove(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity = quantity;
                _db.InventoryItems.Update(inventoryItem);
            }

            _db.SaveChanges();
            return RedirectToPage(new { SelectedCharacterId });
        }

        public IActionResult OnPostRemoveItem(int itemId)
        {
            if (!SelectedCharacterId.HasValue)
            {
                return RedirectToPage();
            }

            var characterId = SelectedCharacterId.Value;
            var character = _db.Characters
                .Include(c => c.Inventory)
                .FirstOrDefault(c => c.Id == characterId);

            if (character?.Inventory == null)
                return RedirectToPage(new { SelectedCharacterId });

            var inventoryId = character.InventoryId;
            if (!inventoryId.HasValue)
                return RedirectToPage(new { SelectedCharacterId });

            var inventoryItem = _db.InventoryItems
                .FirstOrDefault(ii => ii.InventoryId == inventoryId.Value && ii.ItemId == itemId);

            if (inventoryItem != null)
            {
                _db.InventoryItems.Remove(inventoryItem);
                _db.SaveChanges();
            }

            return RedirectToPage(new { SelectedCharacterId });
        }

        private void LoadCharacters()
        {
            Characters = _db.Characters
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();
        }

        private void LoadItems()
        {
            AllItems = _db.Items
                .OrderBy(i => i.Name)
                .ToList();
        }

        private void LoadSelectedCharacter(int characterId)
        {
#pragma warning disable CS8602
            SelectedCharacter = _db.Characters
                .Include(c => c.Inventory)
                .ThenInclude(i => i!.Items)
                .ThenInclude(ii => ii!.Item)
                .ThenInclude(i => i!.ItemType)
                .FirstOrDefault(c => c.Id == characterId);
#pragma warning restore CS8602

            SelectedInventory = SelectedCharacter?.Inventory;
        }
    }
}
