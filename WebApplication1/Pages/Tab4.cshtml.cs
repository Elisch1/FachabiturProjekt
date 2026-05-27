using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class Tab4Model : PageModel
{
    public List<PeopleViewModel> Peoples { get; set; } = new();

    public void OnGet()
    {
        Peoples =
        [
            new()
            {
                Name = "Griesner",
                Subtitle = "Menschen",
                Icon = "🏛️",
                Species = "Menschen",
                Religion = "Griesnertum",
                Origin = "Grisenhav",
                Language = "Thûravin",
                Description = "Die Griesner sind das größte und einflussreichste Menschenvolk..."
            },

            new()
            {
                Name = "Duronai",
                Subtitle = "Zwerge",
                Icon = "⛏️",
                Species = "Zwerge",
                Religion = "Bund der Tiefenväter",
                Origin = "Gebirgskette Ghurazim",
                Language = "Korgund",
                Description = "Die Duronai gelten als Meister der Schmiedekunst..."
            }
        ];
    }
}

public class PeopleViewModel
{
    public string Name { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Species { get; set; } = "";
    public string Religion { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Language { get; set; } = "";
    public string Description { get; set; } = "";
}