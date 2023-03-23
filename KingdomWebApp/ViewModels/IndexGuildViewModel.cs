using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels;

public class IndexGuildViewModel
{
    public IEnumerable<Guild> Guilds { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalGuilds { get; set; }
    public int Category { get; set; }
    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}