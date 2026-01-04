

namespace ExpressVoitures.Models
{
   public class Modele
{
    public int ModeleId { get; set; }
    public string Nom { get; set; }

    public int MarqueId { get; set; }
    public Marque Marque { get; set; }

    public ICollection<Finition> Finitions { get; set; }
    public ICollection<Vehicule> Vehicules { get; set; }
}


}
