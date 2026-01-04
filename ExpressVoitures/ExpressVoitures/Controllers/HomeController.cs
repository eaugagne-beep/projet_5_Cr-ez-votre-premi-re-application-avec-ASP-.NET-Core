using ExpressVoitures.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() // Afficher la liste des véhicules récents
        {
            var vehicules = await _context.Vehicules
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .Include(v => v.Finition)
                .OrderByDescending(v => v.VehiculeId)
                .ToListAsync();

            return View(vehicules);
        }
    }
}
