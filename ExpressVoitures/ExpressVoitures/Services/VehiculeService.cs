using ExpressVoitures.Data;
using ExpressVoitures.Models;
using ExpressVoitures.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Services
{
    // Service pour la gestion des véhicules
    public class VehiculeService : IVehiculeService
    {
        private readonly ApplicationDbContext _context;

        public VehiculeService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Méthodes pour gérer les véhicules
        public async Task<List<Vehicule>> GetAllAsync()
        {
            return await _context.Vehicules
                .Include(v => v.Modele)
                    .ThenInclude(m => m.Marque)
                .Include(v => v.Finition)
                .ToListAsync();
        }
        // Méthode pour récupérer un véhicule par son ID
        public async Task<Vehicule?> GetByIdAsync(int id)
        {
            return await _context.Vehicules
                .Include(v => v.Modele)
                    .ThenInclude(m => m.Marque)
                .Include(v => v.Finition)
                .FirstOrDefaultAsync(v => v.VehiculeId == id);
        }
        // Méthode pour créer un nouveau véhicule
        public async Task CreateAsync(Vehicule vehicule)
        {
            _context.Vehicules.Add(vehicule);
            await _context.SaveChangesAsync();
        }

        // Méthode pour mettre à jour un véhicule existant
        public async Task<List<Modele>> GetModelesAsync()
        {
            return await _context.Modeles.ToListAsync();
        }
        // Méthode pour récupérer toutes les finitions
        public async Task<List<Finition>> GetFinitionsAsync()
        {
            return await _context.Finitions.ToListAsync();
        }
    }
}
