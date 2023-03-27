namespace KingdomWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int SupplyId { get; set; }
        public Supply Supply { get; set; }
        public int? ReviewerId { get; set; }
        public Reviewer? Reviewer { get; set; }
        //public ICollection<Review>? Reviews { get; set;}
        public ICollection<Reviewer>? Reviewers { get; set; }
    }
}