using KingdomWebApp.Models;

namespace KingdomWebApp.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}