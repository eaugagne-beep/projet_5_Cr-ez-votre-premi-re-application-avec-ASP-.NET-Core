namespace ExpressVoitures.Models
{
    public class Finition
    {
        public int FinitionId { get; set; }
        public string Nom { get; set; }

        public int ModeleId { get; set; }
        public Modele Modele { get; set; }
    }
}
