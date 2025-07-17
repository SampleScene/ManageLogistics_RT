using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Repository
{
    public class StopRepository : IStopRepository
    {
        private readonly ApplicationDbContext _context;

        public StopRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Stop stop)
        {
            _context.stops.Add(stop);
            return Save();
        }

        public bool Delete(Stop stop)
        {
            _context.stops.Remove(stop);
            return Save();
        }

        public async Task<IEnumerable<Stop>> GetAll()
        {
            return await _context.stops.ToListAsync();
        } 

        public async Task<Stop> GetByIdAsync(int id)
        {
            return await _context.stops.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stop> GetByIdAsyncNoTracking(int id)
        {
            return await _context.stops.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Stop stop)
        {
            _context.Update(stop);
            return Save();
        }
    }
}
