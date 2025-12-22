namespace ExpressVoitures.Models
{
    public class Achat
    {
        public int AchatId { get; set; }
        public DateTime DateAchat { get; set; }
        public decimal PrixAchat { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
