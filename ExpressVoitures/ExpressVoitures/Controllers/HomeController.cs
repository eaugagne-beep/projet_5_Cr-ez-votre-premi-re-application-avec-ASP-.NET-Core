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

        // Afficher la liste des véhicules récents
        public async Task<IActionResult> Index() 
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
