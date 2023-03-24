using KingdomWebApp.Models;

namespace KingdomWebApp.ViewModels;

public class IndexTradeViewModel
{
    public IEnumerable<Trade> Trades { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalTrades { get; set; }
    public int Category { get; set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}