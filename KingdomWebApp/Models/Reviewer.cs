namespace KingdomWebApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}