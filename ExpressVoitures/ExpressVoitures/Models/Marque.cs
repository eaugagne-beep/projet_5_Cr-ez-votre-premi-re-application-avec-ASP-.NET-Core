namespace ExpressVoitures.Models
{
    public class Marque
    {
        public int MarqueId { get; set; }
        public string Nom { get; set; }

        public List<Modele> Modeles { get; set; }
    }
}
