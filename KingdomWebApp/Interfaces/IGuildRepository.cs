using KingdomWebApp.Models;
using KingdomWebApp.Models.Enum;

namespace KingdomWebApp.Interfaces
{
    public interface IGuildRepository
    {
        Task<IEnumerable<Guild>> GetAll();

        Task<IEnumerable<Guild>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Guild>> GetGuildsByState(string state);

        Task<IEnumerable<Guild>> GetGuildsByCategoryAndSliceAsync(GuildCategory category, int offset, int size);

        Task<List<State>> GetAllStates();

        Task<List<City>> GetAllCitiesByState(string state);

        Task<Guild?> GetByIdAsync(int id);

        Task<Guild?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Guild>> GetGuildByCity(string city);

        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(GuildCategory category);

        bool Add(Guild guild);

        bool Update(Guild guild);

        bool Delete(Guild guild);

        bool Save();
    }
}