using ExpressVoitures.Models;

namespace ExpressVoitures.Services.Interfaces
{
    // Interface pour le service de gestion des véhicules
    public interface IVehiculeService
    {
        Task<List<Vehicule>> GetAllAsync();
        Task<Vehicule?> GetByIdAsync(int id);
        Task CreateAsync(Vehicule vehicule);

        // Méthodes pour récupérer les modèles et finitions
        Task<List<Modele>> GetModelesAsync();
        Task<List<Finition>> GetFinitionsAsync();
    }
}
