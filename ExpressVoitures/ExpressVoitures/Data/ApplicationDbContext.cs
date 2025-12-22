using ExpressVoitures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Achat> Achats { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Vente> Ventes { get; set; }
    }
}
