using System;

namespace ExpressVoitures.Models
{
    public class Vehicule
    {
        public int VehiculeId { get; set; }
        public string CodeVIN { get; set; }
        public int Annee { get; set; }

        public int MarqueId { get; set; }
        public Marque Marque { get; set; }

        public int ModeleId { get; set; }
        public Modele Modele { get; set; }

        public int FinitionId { get; set; }
        public Finition Finition { get; set; }

        public Achat Achat { get; set; }
        public Vente Vente { get; set; }

        public List<Reparation> Reparations { get; set; }
    }
}
