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

        // DbSets pour les entités
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<TypeReparation> TypeReparations { get; set; }
        public DbSet<Reparation> Reparations { get; set; }

        // Configuration des relations et des contraintes
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<Vehicule>()
                .HasOne(v => v.Modele)
                .WithMany()
                .HasForeignKey(v => v.ModeleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicule>()
                .HasOne(v => v.Finition)
                .WithMany()
                .HasForeignKey(v => v.FinitionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optionnel mais recommandé
            builder.Entity<Vehicule>()
                .Property(v => v.PrixAchat)
                .HasPrecision(18, 2);

            builder.Entity<Vehicule>()
                .Property(v => v.PrixVente)
                .HasPrecision(18, 2);

            builder.Entity<Marque>().HasData(
                 new Marque { MarqueId = 1, Nom = "Mazda" },
                 new Marque { MarqueId = 2, Nom = "Jeep" },
                 new Marque { MarqueId = 3, Nom = "Renault" },
                 new Marque { MarqueId = 4, Nom = "Ford" },
                 new Marque { MarqueId = 5, Nom = "Honda" },
                 new Marque { MarqueId = 6, Nom = "Volkswagen" }

            );

            builder.Entity<Modele>().HasData(
                new Modele { ModeleId = 1, Nom = "Miata", MarqueId = 1 },
                new Modele { ModeleId = 2, Nom = "Liberty", MarqueId = 2 },
                new Modele { ModeleId = 3, Nom = "Scénic", MarqueId = 3 },
                new Modele { ModeleId = 4, Nom = "Explorer", MarqueId = 4 },
                new Modele { ModeleId = 5, Nom = "Civic", MarqueId = 5 },
                new Modele { ModeleId = 6, Nom = "GTI", MarqueId = 6 },
                new Modele { ModeleId = 7, Nom = "Edge", MarqueId = 4 }

            );

            builder.Entity<Finition>().HasData(
                new Finition { FinitionId = 1, Nom = "LE", ModeleId = 1 },
                new Finition { FinitionId = 2, Nom = "Sport", ModeleId = 2 },
                new Finition { FinitionId = 3, Nom = "TCe", ModeleId = 3 },
                new Finition { FinitionId = 4, Nom = "XLT", ModeleId = 4 },
                new Finition { FinitionId = 5, Nom = "LX", ModeleId = 5 },
                new Finition { FinitionId = 6, Nom = "S", ModeleId = 6 },
                new Finition { FinitionId = 7, Nom = "SEL", ModeleId = 7 }
            );
        }

    }



    
}