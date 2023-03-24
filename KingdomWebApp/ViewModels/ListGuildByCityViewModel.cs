using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels
{
    public class ListGuildByCityViewModel
    {
        public IEnumerable<Guild> Guilds { get; set; }
        public bool NoGuildWarning { get; set; } = false;
        public string City { get; set; }
        public string State { get; set; }
    }
}
