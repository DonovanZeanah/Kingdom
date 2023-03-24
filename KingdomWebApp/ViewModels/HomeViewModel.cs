using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Guild>? Guilds { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}