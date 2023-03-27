namespace KingdomWebApp.Models
{
    // Models/Rating.cs
    public class Rating
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int Score { get; set; }
        public Skill Skill { get; set; }
    }
}

/*
 public class Rating
{
    public int Id { get; set; }
    public int SkillId { get; set; }
    public int Score { get; set; }
    public Skill Skill { get; set; }
}
 */
