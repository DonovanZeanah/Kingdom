using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Data;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;

namespace KingdomWebApp.Repository
{
    public class GuildRepository : IGuildRepository
    {
        private readonly ApplicationDbContext _context;

        public GuildRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Guild guild)
        {
            _context.Add(guild);
            return Save();
        }

        public bool Delete(Guild guild)
        {
            _context.Remove(guild);
            return Save();
        }

        public async Task<IEnumerable<Guild>> GetAll()
        {
            return await _context.Guilds.ToListAsync();
        }

        public async Task<List<State>> GetAllStates()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<IEnumerable<Guild>> GetSliceAsync(int offset, int size)
        {
            return await _context.Guilds.Include(i => i.Address).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Guild>> GetGuildsByCategoryAndSliceAsync(GuildCategory category, int offset, int size)
        {
            return await _context.Guilds
                .Include(i => i.Address)
                .Where(c => c.GuildCategory == category)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(GuildCategory category)
        {
            return await _context.Guilds.CountAsync(c => c.GuildCategory == category);
        }

        public async Task<Guild?> GetByIdAsync(int id)
        {
            return await _context.Guilds.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Guild?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Guilds.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Guild>> GetGuildByCity(string city)
        {
            return await _context.Guilds.Where(c => c.Address.City.Contains(city)).Distinct().ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Guild guild)
        {
            _context.Update(guild);
            return Save();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Guilds.CountAsync();
        }

        public async Task<IEnumerable<Guild>> GetGuildsByState(string state)
        {
            return await _context.Guilds.Where(c => c.Address.State.Contains(state)).ToListAsync();
        }

        public async Task<List<City>> GetAllCitiesByState(string state)
        {
            return await _context.Cities.Where(c => c.StateCode.Contains(state)).ToListAsync();
        }
    }
}