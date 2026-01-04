
using System.ComponentModel.DataAnnotations;


namespace ExpressVoitures.Models
{
    public class Vehicule
    {
        public int VehiculeId { get; set; }

        public string CodeVIN { get; set; }

        [Range(1990, 2100)]
        public int Annee { get; set; }

        public int MarqueId { get; set; }
       
        public Marque? Marque { get; set; }

        public int ModeleId { get; set; }
        public Modele? Modele { get; set; }

       
        public int FinitionId { get; set; }
        public Finition? Finition { get; set; }

     
        [DataType(DataType.Date)]
        
        public DateTime DateAchat { get; set; }
        public decimal PrixAchat { get; set; }

       
        [DataType(DataType.Date)]
        
        public DateTime? DateVente { get; set; }
        public decimal? PrixVente { get; set; }

        // Indique si le véhicule est vendu ou disponible
        public bool EstVendu { get; set; } 

        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        


    }




}
