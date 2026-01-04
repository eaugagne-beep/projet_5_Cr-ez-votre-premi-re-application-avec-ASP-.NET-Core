using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    public class Reparation
    {
        public int ReparationId { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }

        public int? TypeReparationId { get; set; }
        public TypeReparation? TypeReparation { get; set; }

        public decimal Cout { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }  
}
