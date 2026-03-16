using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebApplication1.Models;
using System.ComponentModel;

namespace WebApplication1.Pages
{
    public class Tab1Model : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public List<Character> Characters { get; set; } = new();

        [BindProperty]
        public Character NewCharacter { get; set; } = new();

        [BindProperty]
        public IFormFile? UploadImage { get; set; }

        public Tab1Model(AppDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public void OnGet()
        {
            LoadCharacters();
        }

        public void OnGetEdit(int id)
        {
            NewCharacter = _db.Characters.FirstOrDefault(c => c.Id == id) ?? new();
            LoadCharacters();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadCharacters();
                return Page();
            }

            if (NewCharacter.Id == 0)
            {
                // Create
                NewCharacter.Type = CharacterType.Player;
                _db.Characters.Add(NewCharacter);
            }
            else
            {
                // Update
                var existing = _db.Characters.FirstOrDefault(c => c.Id == NewCharacter.Id);
                if (existing != null)
                {
                    existing.FirstName = NewCharacter.FirstName;
                    existing.LastName = NewCharacter.LastName;
                    existing.Age = NewCharacter.Age;
                    existing.Description = NewCharacter.Description;
                    _db.Characters.Update(existing);
                }
            }
            if (UploadImage != null)
            {
                string webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string uploadsFolder = Path.Combine(webRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadImage.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    UploadImage.CopyTo(stream);
                }

                NewCharacter.ProfileImagePath = "/uploads/" + fileName;
            }

            _db.SaveChanges();
            NewCharacter = new();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var character = _db.Characters.FirstOrDefault(c => c.Id == id);
            if (character != null)
            {
                _db.Characters.Remove(character);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }

        private void LoadCharacters()
        {
            Characters = _db.Characters.ToList();
        }

    }
}