namespace KingdomWebApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SupplyCategory> SupplyCategories { get; set; }
    }
}
