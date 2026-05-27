using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class Tab2Model : PageModel
    {
        private readonly AppDbContext _db;

        public List<Character> Characters { get; set; } = new();
        public List<Kampf> ActiveBattles { get; set; } = new();
        public Kampf? CurrentBattle { get; set; }
        public KampfTeilnehmer? CurrentAttacker { get; set; }
        public KampfTeilnehmer? CurrentDefender { get; set; }
        public bool AwaitingNextAttackerSelection { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public int? BattleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentAttackerIndex { get; set; } = 0;

        [BindProperty]
        public List<int> SelectedCharacterIds { get; set; } = new();

        [BindProperty]
        public int AttackRoll { get; set; }

        [BindProperty]
        public int DefenseRoll { get; set; }

        [BindProperty]
        public int? SelectedDefenderId { get; set; }

        [BindProperty]
        public int? SelectedNextAttackerId { get; set; }

        [BindProperty]
        public Dictionary<int, int> ManualInitiatives { get; set; } = new();

        public Tab2Model(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            LoadCharacters();

            if (BattleId.HasValue)
            {
                LoadBattle(BattleId.Value);
            }
        }

        public IActionResult OnPostStartBattle()
        {
            LoadCharacters();

            if (SelectedCharacterIds == null || SelectedCharacterIds.Count < 2 || SelectedCharacterIds.Count > 5)
            {
                ModelState.AddModelError(string.Empty, "Bitte wähle 2-5 unterschiedliche Charaktere aus.");
                return Page();
            }

            if (SelectedCharacterIds.Distinct().Count() != SelectedCharacterIds.Count)
            {
                ModelState.AddModelError(string.Empty, "Du musst unterschiedliche Charaktere wählen.");
                return Page();
            }

            var selectedCharacters = _db.Characters
                .Where(c => SelectedCharacterIds.Contains(c.Id))
                .ToList();

            if (selectedCharacters.Count != SelectedCharacterIds.Count)
            {
                ModelState.AddModelError(string.Empty, "Einer oder mehrere Charaktere konnten nicht gefunden werden.");
                return Page();
            }

            var kampf = new Kampf
            {
                Date = DateTime.Now,
                Status = KampfStatus.Aktiv
            };

            _db.Kämpfe.Add(kampf);
            _db.SaveChanges();

            foreach (var character in selectedCharacters)
            {
                var teilnehmer = new KampfTeilnehmer
                {
                    KampfId = kampf.Id,
                    CharacterId = character.Id,
                    CurrentHp = 100,
                    Initiative = 0
                };
                _db.KampfTeilnehmer.Add(teilnehmer);
            }

            _db.SaveChanges();

            return RedirectToPage(new { BattleId = kampf.Id });
        }

        // Automatisch würfeln – Server würfelt für alle
        public IActionResult OnPostRollInitiative()
        {
            if (!BattleId.HasValue)
                return RedirectToPage();

            LoadBattle(BattleId.Value);

            if (CurrentBattle == null || CurrentBattle.Teilnehmer.Count == 0)
                return RedirectToPage();

            var rng = new Random();
            foreach (var teilnehmer in CurrentBattle.Teilnehmer)
            {
                if (teilnehmer.Character != null)
                {
                    int roll = rng.Next(1, 21);
                    teilnehmer.Initiative = roll + (teilnehmer.Character.Agility / 3);
                }
            }

            _db.SaveChanges();

            var sorted = CurrentBattle.Teilnehmer.OrderByDescending(t => t.Initiative).ToList();

            var log = new KampfLog
            {
                KampfId = CurrentBattle.Id,
                Nachricht = "Initiative-Phase (automatisch): " + string.Join(", ",
                    sorted.Select(t => $"{t.Character?.FirstName} ({t.Initiative})"))
            };
            _db.KampfLogs.Add(log);
            _db.SaveChanges();

            return RedirectToPage(new { BattleId, CurrentAttackerIndex = 0 });
        }

        // Manuell eingeben – Spieler gibt d20-Ergebnis pro Charakter ein
        public IActionResult OnPostRollInitiativeManual()
        {
            if (!BattleId.HasValue)
                return RedirectToPage();

            LoadBattle(BattleId.Value);

            if (CurrentBattle == null || CurrentBattle.Teilnehmer.Count == 0)
                return RedirectToPage();

            // Validierung: für jeden Teilnehmer muss ein Wert vorhanden sein
            if (ManualInitiatives == null || ManualInitiatives.Count != CurrentBattle.Teilnehmer.Count)
            {
                ModelState.AddModelError(string.Empty, "Bitte für alle Teilnehmer einen Würfelwert eingeben.");
                return Page();
            }

            foreach (var kvp in ManualInitiatives)
            {
                if (kvp.Value < 1 || kvp.Value > 20)
                {
                    ModelState.AddModelError(string.Empty, "Alle Würfelwerte müssen zwischen 1 und 20 liegen.");
                    return Page();
                }
            }

            // Initiative setzen: manueller Wurf + Agilität-Bonus (identisch zur automatischen Variante)
            foreach (var teilnehmer in CurrentBattle.Teilnehmer)
            {
                if (ManualInitiatives.TryGetValue(teilnehmer.CharacterId, out int roll) && teilnehmer.Character != null)
                {
                    teilnehmer.Initiative = roll + (teilnehmer.Character.Agility / 3);
                }
            }

            _db.SaveChanges();

            var sorted = CurrentBattle.Teilnehmer.OrderByDescending(t => t.Initiative).ToList();

            var log = new KampfLog
            {
                KampfId = CurrentBattle.Id,
                Nachricht = "Initiative-Phase (manuell): " + string.Join(", ",
                    sorted.Select(t => $"{t.Character?.FirstName} ({t.Initiative})"))
            };
            _db.KampfLogs.Add(log);
            _db.SaveChanges();

            return RedirectToPage(new { BattleId, CurrentAttackerIndex = 0 });
        }

        public IActionResult OnPostAttack()
        {
            if (!BattleId.HasValue || AttackRoll < 1 || AttackRoll > 20 || DefenseRoll < 1 || DefenseRoll > 20 || !SelectedDefenderId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Ungültige Eingabe. Würfelergebnisse müssen zwischen 1 und 20 liegen, und ein Verteidiger muss gewählt sein.");
                if (BattleId.HasValue)
                    LoadBattle(BattleId.Value);
                return Page();
            }

            LoadBattle(BattleId.Value);

            if (CurrentBattle == null || CurrentAttacker == null)
                return RedirectToPage();

            var attacker = _db.KampfTeilnehmer
                .Include(kt => kt.Character)
                .FirstOrDefault(kt => kt.KampfId == BattleId.Value && kt.CharacterId == CurrentAttacker.CharacterId);

            var defender = _db.KampfTeilnehmer
                .Include(kt => kt.Character)
                .FirstOrDefault(kt => kt.KampfId == BattleId.Value && kt.CharacterId == SelectedDefenderId.Value);

            if (attacker == null || defender == null || attacker.Character == null || defender.Character == null)
            {
                ModelState.AddModelError(string.Empty, "Fehler beim Laden der Charakterdaten.");
                return Page();
            }

            int attackValue = AttackRoll + (attacker.Character.Precision / 3);
            int defenseValue = 10 + DefenseRoll + (defender.Character.Agility / 4);

            var log = new KampfLog
            {
                KampfId = CurrentBattle.Id,
                AngreiferId = attacker.CharacterId,
                VerteidigerId = defender.CharacterId,
                Nachricht = $"{attacker.Character.FirstName} greift an: Würfel {AttackRoll} + Genauigkeit {attacker.Character.Precision / 3} = {attackValue} vs. " +
                            $"{defender.Character.FirstName} Abwehr: Würfel {DefenseRoll} + Beweglichkeit {defender.Character.Agility / 4} = {defenseValue}"
            };

            if (attackValue >= defenseValue)
            {
                int baseDamage = new Random().Next(5, 15);
                int damage = baseDamage + (attacker.Character.Strength / 5);
                defender.CurrentHp -= damage;

                log.Nachricht += $" - TREFFER! Schaden: {damage}. HP: {Math.Max(0, defender.CurrentHp)}";

                if (defender.CurrentHp <= 0)
                {
                    defender.CurrentHp = 0;

                    // Prüfe wie viele Kämpfer noch leben (ohne den gerade besiegten)
                    var alive = CurrentBattle.Teilnehmer.Where(t => t.CharacterId != defender.CharacterId && t.CurrentHp > 0).ToList();
                    if (alive.Count <= 0)
                    {
                        log.Nachricht += $"\n🎉 {attacker.Character.FirstName} {attacker.Character.LastName} hat gewonnen!";
                        CurrentBattle.Status = KampfStatus.Beendet;
                    }
                    else
                    {
                        log.Nachricht += $"\n💀 {defender.Character.FirstName} wurde besiegt und scheidet aus!";
                    }
                }
            }
            else
            {
                log.Nachricht += " - VERFEHLT!";
            }

            _db.KampfLogs.Add(log);
            _db.SaveChanges();

            int nextIndex = CurrentAttackerIndex;
            if (CurrentBattle.Status != KampfStatus.Beendet)
            {
                // Überspringe besiegte Kämpfer beim Weiterschalten
                var sorted = CurrentBattle.Teilnehmer.OrderByDescending(t => t.Initiative).ToList();
                int attempts = 0;
                do
                {
                    nextIndex = (nextIndex + 1) % sorted.Count;
                    attempts++;
                } while (sorted[nextIndex].CurrentHp <= 0 && attempts < sorted.Count);
            }

            return RedirectToPage(new { BattleId, CurrentAttackerIndex = nextIndex });
        }

        public IActionResult OnPostEndBattle()
        {
            if (!BattleId.HasValue)
                return RedirectToPage();

            LoadBattle(BattleId.Value);

            if (CurrentBattle != null)
            {
                CurrentBattle.Status = KampfStatus.Beendet;
                _db.SaveChanges();
            }

            return RedirectToPage(new { BattleId });
        }

        private void LoadBattle(int battleId)
        {
            CurrentBattle = _db.Kämpfe
                .Include(k => k.Teilnehmer)
                .ThenInclude(kt => kt.Character)
                .Include(k => k.Logs)
                .FirstOrDefault(k => k.Id == battleId);

            if (CurrentBattle != null && CurrentBattle.Teilnehmer.Any())
            {
                var sorted = CurrentBattle.Teilnehmer.OrderByDescending(t => t.Initiative).ToList();

                if (CurrentAttackerIndex >= 0 && CurrentAttackerIndex < sorted.Count)
                {
                    CurrentAttacker = sorted[CurrentAttackerIndex];
                }
            }
        }

        private void LoadCharacters()
        {
            Characters = _db.Characters.ToList();
        }
    }
}