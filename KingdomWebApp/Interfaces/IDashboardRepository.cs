using KingdomWebApp.Models;

namespace KingdomWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Trade>> GetAllUserTrades();
        Task<List<Guild>> GetAllUserGuilds();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
