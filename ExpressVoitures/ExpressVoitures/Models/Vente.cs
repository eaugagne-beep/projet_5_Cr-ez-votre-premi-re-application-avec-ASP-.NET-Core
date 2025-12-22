namespace ExpressVoitures.Models
{
    public class Vente
    {
        public int VenteId { get; set; }
        public DateTime DateDisponibilite { get; set; }
        public decimal PrixVente { get; set; }
        public DateTime? DateVente { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
