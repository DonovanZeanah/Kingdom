namespace KingdomWebApp.Models
{
    public class Material
    {
        public int Id { get; set; }

        ICollection<Tool> Tools { get; set; }
    }
}