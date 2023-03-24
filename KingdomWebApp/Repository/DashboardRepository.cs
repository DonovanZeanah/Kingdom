using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Data;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;

namespace KingdomWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Guild>> GetAllUserGuilds()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGuilds = _context.Guilds.Where(r => r.AppUser.Id == curUser);
            return userGuilds.ToList();
        }

        public async Task<List<Trade>> GetAllUserTrades()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTrades = _context.Trades.Where(r => r.AppUser.Id == curUser);
            return userTrades.ToList();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
