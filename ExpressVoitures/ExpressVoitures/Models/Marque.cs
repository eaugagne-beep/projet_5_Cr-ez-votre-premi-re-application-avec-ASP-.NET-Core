namespace ExpressVoitures.Models
{
    public class Marque
    {
        public int MarqueId { get; set; }
        public string Nom { get; set; }

        public ICollection<Modele> Modeles { get; set; }
    }

}
