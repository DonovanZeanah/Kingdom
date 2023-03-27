using System.Collections;
using KingdomWebApp.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomWebApp.Models
{
    public class Guild
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public GuildCategory GuildCategory { get; set; }

        public GuildSubcategory GuildSubcategory { get; set; }
        public State State { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
    }
}
