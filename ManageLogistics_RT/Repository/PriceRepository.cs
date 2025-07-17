using ManageLogistics_RT.Data.Enum;
using ManageLogistics_RT.Data;
using ManageLogistics_RT.Models;
using Microsoft.EntityFrameworkCore;
using ManageLogistics_RT.Interface;

namespace ManageLogistics_RT.Repository
{
    public class PriceRepository : IPriceRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Price price)
        {
            _context.Add(price);
            return Save();
        }

        public bool Delete(Price price)
        {
            _context.Remove(price);
            return Save();
        }

        public async Task<IEnumerable<Price>> GetAll()
        {
            return await _context.prices.ToListAsync();
        }

        public async Task<Price> GetByIdAsync(int id)
        {
            return await _context.prices.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Price> GetByIdAsyncNoTracking(int id)
        {
            return await _context.prices.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        
        
        public async Task<IEnumerable<Price>> GetPriceByType(TypePrice type)
        {
            return await _context.prices.Where(t => t.Type == type).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        
        public bool Update(Price price)
        {
            _context.Update(price);
            return Save();
        }
    }
}
