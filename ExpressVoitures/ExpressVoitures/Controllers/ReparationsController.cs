using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ExpressVoitures.Controllers
{
    [Authorize(Roles = "Admin")] // Restreindre l'accès aux administrateurs
    public class ReparationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReparationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reparations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reparations.Include(r => r.TypeReparation).Include(r => r.Vehicule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reparations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparations
                .Include(r => r.TypeReparation)
                .Include(r => r.Vehicule)
                .FirstOrDefaultAsync(m => m.ReparationId == id);
            if (reparation == null)
            {
                return NotFound();
            }

            return View(reparation);
        }

        // GET: Reparations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "CodeVIN");
            ViewData["TypeReparationId"] = new SelectList(_context.TypeReparations, "TypeReparationId", "Nom");
            return View();
        }


        // POST: Reparations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Reparation reparation, string? NouveauType)
        {
            // si l'utilisateur a écrit un nouveau type
            if (!string.IsNullOrWhiteSpace(NouveauType))
            {
                var existing = await _context.TypeReparations
                    .FirstOrDefaultAsync(t => t.Nom == NouveauType);


                if (existing == null)
                {
                    var type = new TypeReparation { Nom = NouveauType };
                    _context.TypeReparations.Add(type);
                    await _context.SaveChangesAsync();

                    reparation.TypeReparationId = type.TypeReparationId;
                }
                else
                {
                    reparation.TypeReparationId = existing.TypeReparationId;
                }
            }

            // valider et enregistrer
            if (ModelState.IsValid)
            {
                _context.Reparations.Add(reparation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // si erreur, recharger les listes
            ViewBag.TypeReparationId = new SelectList(_context.TypeReparations, "TypeReparationId", "Nom", reparation.TypeReparationId);
            ViewBag.VehiculeId = new SelectList(_context.Vehicules, "VehiculeId", "CodeVIN", reparation.VehiculeId);
            return View(reparation);
        }




        // GET: Reparations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            // chercher la réparation
            var reparation = await _context.Reparations.FindAsync(id);
            if (reparation == null)
            {
                return NotFound();
            }
            ViewData["TypeReparationId"] = new SelectList(_context.TypeReparations, "TypeReparationId", "Nom", reparation.TypeReparationId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "CodeVIN", reparation.VehiculeId);
            return View(reparation);
        }

        // POST: Reparations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ReparationId,VehiculeId,TypeReparationId,Cout,Date")] Reparation reparation)
        {
            
            if (id != reparation.ReparationId)
            {
                return NotFound();
            }

            // valider et enregistrer
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reparation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparationExists(reparation.ReparationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeReparationId"] = new SelectList(_context.TypeReparations, "TypeReparationId", "Nom", reparation.TypeReparationId);
            ViewData["VehiculeId"] = new SelectList(_context.Vehicules, "VehiculeId", "CodeVIN", reparation.VehiculeId);
            return View(reparation);
        }

        // GET: Reparations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparations
                .Include(r => r.TypeReparation)
                .Include(r => r.Vehicule)
                .FirstOrDefaultAsync(m => m.ReparationId == id);
            if (reparation == null)
            {
                return NotFound();
            }

            return View(reparation);
        }

        // POST: Reparations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparation = await _context.Reparations.FindAsync(id);
            if (reparation != null)
            {
                _context.Reparations.Remove(reparation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReparationExists(int id)
        {
            return _context.Reparations.Any(e => e.ReparationId == id);
        }
    }
}
