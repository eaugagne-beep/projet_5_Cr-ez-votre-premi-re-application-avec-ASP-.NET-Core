namespace ExpressVoitures.Models
{
    public class Reparation
    {
        public int ReparationId { get; set; }
        public string Description { get; set; }
        public decimal Cout { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
