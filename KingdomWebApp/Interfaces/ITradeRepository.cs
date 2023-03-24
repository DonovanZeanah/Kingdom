using KingdomWebApp.Models;
using KingdomWebApp.Models.Enum;

namespace KingdomWebApp.Interfaces
{
    public interface ITradeRepository
    {
        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(TradeCategory category);

        Task<Trade?> GetByIdAsync(int id);

        Task<Trade?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Trade>> GetAll();

        Task<IEnumerable<Trade>> GetAllTradesByCity(string city);

        Task<IEnumerable<Trade>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Trade>> GetTradesByCategoryAndSliceAsync(TradeCategory category, int offset, int size);

        bool Add(Trade trade);

        bool Update(Trade trade);

        bool Delete(Trade trade);

        bool Save();
    }
}