using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ExpressVoitures.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public VehiculesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }





        // GET: Vehicules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicules.Include(v => v.Finition).Include(v => v.Marque).Include(v => v.Modele);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules
                .Include(v => v.Finition)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .FirstOrDefaultAsync(m => m.VehiculeId == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicules/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MarqueId"] = new SelectList(_context.Marques, "MarqueId", "Nom");
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "Nom");
            ViewData["FinitionId"] = new SelectList(_context.Finitions, "FinitionId", "Nom");

            return View();
        }

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Vehicule vehicule, IFormFile? imageFile, bool estVenduChoisi)

        {
            

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);

                    vehicule.ImagePath = "/uploads/" + fileName;
                }

                vehicule.EstVendu = estVenduChoisi;
                if (estVenduChoisi)
                {
                    vehicule.DateVente ??= DateTime.Today;
                }
                else
                {
                    vehicule.DateVente = null;
                }

                _context.Add(vehicule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // IMPORTANT : recharger les dropdown si erreur validation
            ViewData["MarqueId"] = new SelectList(_context.Marques, "MarqueId", "Nom", vehicule.MarqueId);
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "Nom", vehicule.ModeleId);
            ViewData["FinitionId"] = new SelectList(_context.Finitions, "FinitionId", "Nom", vehicule.FinitionId);
            return View(vehicule);
        }




        // GET: Vehicules/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }
            ViewData["MarqueId"] = new SelectList(_context.Marques, "MarqueId", "Nom");
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "Nom");
            ViewData["FinitionId"] = new SelectList(_context.Finitions, "FinitionId", "Nom");

            return View(vehicule);
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Vehicule vehicule, IFormFile? imageFile, bool estVenduChoisi)

        {
            if (id != vehicule.VehiculeId) return NotFound();

            var vehiculeDb = await _context.Vehicules.FirstOrDefaultAsync(v => v.VehiculeId == id);
            if (vehiculeDb == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["FinitionId"] = new SelectList(_context.Finitions, "FinitionId", "Nom", vehicule.FinitionId);
                ViewData["MarqueId"] = new SelectList(_context.Marques, "MarqueId", "Nom", vehicule.MarqueId);
                ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "Nom", vehicule.ModeleId);
                return View(vehicule);
            }

            // copier champs
            vehiculeDb.CodeVIN = vehicule.CodeVIN;
            vehiculeDb.Annee = vehicule.Annee;
            vehiculeDb.MarqueId = vehicule.MarqueId;
            vehiculeDb.ModeleId = vehicule.ModeleId;
            vehiculeDb.FinitionId = vehicule.FinitionId;

            vehiculeDb.DateAchat = vehicule.DateAchat;
            vehiculeDb.PrixAchat = vehicule.PrixAchat;
            vehiculeDb.DateVente = vehicule.DateVente;
            vehiculeDb.PrixVente = vehicule.PrixVente;

            vehiculeDb.Description = vehicule.Description;

            vehiculeDb.EstVendu = estVenduChoisi;
            if (estVenduChoisi)
            {
                vehiculeDb.DateVente ??= DateTime.Today; // si vendu et aucune date, on met aujourd’hui
            }
            else
            {
                vehiculeDb.DateVente = null; // dispo => pas de date de vente
            }



            // image : seulement si nouveau fichier
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                vehiculeDb.ImagePath = "/uploads/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: Vehicules/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicules
                .Include(v => v.Finition)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .FirstOrDefaultAsync(m => m.VehiculeId == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicules/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule != null)
            {
                _context.Vehicules.Remove(vehicule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculeExists(int id)
        {
            return _context.Vehicules.Any(e => e.VehiculeId == id);
        }
    }
}
