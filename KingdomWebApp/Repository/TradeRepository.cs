using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Data;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;

namespace KingdomWebApp.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private readonly ApplicationDbContext _context;

        public TradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Trade trade)
        {
            _context.Add(trade);
            return Save();
        }

        public bool Delete(Trade trade)
        {
            _context.Remove(trade);
            return Save();
        }

        public async Task<IEnumerable<Trade>> GetAll()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<IEnumerable<Trade>> GetAllTradesByCity(string city)
        {
            return await _context.Trades.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await _context.Trades.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trade?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Trades.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Trades.CountAsync();
        }

        public async Task<int> GetCountByCategoryAsync(TradeCategory category)
        {
            return await _context.Trades.CountAsync(r => r.TradeCategory == category);
        }

        public async Task<IEnumerable<Trade>> GetSliceAsync(int offset, int size)
        {
            return await _context.Trades.Include(a => a.Address).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Trade>> GetTradesByCategoryAndSliceAsync(TradeCategory category, int offset, int size)
        {
            return await _context.Trades
                .Where(r => r.TradeCategory == category)
                .Include(a => a.Address)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Trade trade)
        {
            _context.Update(trade);
            return Save();
        }
    }
}