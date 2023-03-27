using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomWebApp.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Tool> Tools { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Description { get; set; }
        public ICollection<Rating>? Ratings { get; set; }


    }
}


/*
 
 public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}

// Models/Rating.cs


*/