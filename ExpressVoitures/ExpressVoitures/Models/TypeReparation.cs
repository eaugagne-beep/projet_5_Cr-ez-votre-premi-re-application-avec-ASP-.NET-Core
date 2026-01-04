namespace ExpressVoitures.Models
{
    public class TypeReparation
    {
        public int TypeReparationId { get; set; }
        public string Nom { get; set; }
        public string? Description { get; set; }

        public ICollection<Reparation> Reparations { get; set; }
    }
}
