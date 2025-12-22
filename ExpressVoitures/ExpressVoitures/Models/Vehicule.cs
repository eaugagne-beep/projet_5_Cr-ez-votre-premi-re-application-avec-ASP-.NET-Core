using System;

namespace ExpressVoitures.Models
{
    public class Vehicule
    {
        public int VehiculeId { get; set; }
        public string CodeVIN { get; set; }
        public int Annee { get; set; }

        public int FinitionId { get; set; }
        public Finition Finition { get; set; }

        public Achat Achat { get; set; }
        public Vente Vente { get; set; }

        public List<Reparation> Reparations { get; set; }
    }
}
