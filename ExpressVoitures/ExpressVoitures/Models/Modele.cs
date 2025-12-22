using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressVoitures.Models
{
    public class Modele
    {
        public int ModeleId { get; set; }
        public string Nom { get; set; }

        public int MarqueId { get; set; }
        public Marque Marque { get; set; }

        public List<Finition> Finitions { get; set; }
    }
}
