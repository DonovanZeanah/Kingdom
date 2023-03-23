using KingdomWebApp.Data.Enum;
using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels
{
    public class CreateGuildViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public GuildCategory GuildCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
