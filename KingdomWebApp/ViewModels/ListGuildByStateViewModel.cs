using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels
{
    public class ListGuildByStateViewModel
    {
        public IEnumerable<Guild> Guilds { get; set; }
        public bool NoGuildWarning { get; set; } = false;
        public string State { get; set; }
    }
}
