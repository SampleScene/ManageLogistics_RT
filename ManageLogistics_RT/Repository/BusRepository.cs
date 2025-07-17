using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ManageLogistics_RT.Repository
{
    public class BusRepository : IBusRepository
    {
        private readonly ApplicationDbContext _contet;

        public BusRepository(ApplicationDbContext context) 
        {
            _contet = context;
        }

        public async Task<IEnumerable<Bus>> GetAll()
        {
            return await _contet.Buses.ToListAsync();
        }

        public async Task<Bus> GetById(int id)
        {
            return await _contet.Buses.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Bus>> GetByModel(string model)
        {
            return await _contet.Buses.Where(m => m.Model.Contains(model)).ToListAsync();
        }

        public async Task<IEnumerable<Bus>> GetByTerminalId(int terminalId)
        {
            return await _contet.Buses.Where(m => m.TerminalId == terminalId).ToListAsync();
        }
        public bool Add(Bus bus)
        {
            _contet.Add(bus);
            return Save();
        }

        public bool Delete(Bus bus)
        {
            _contet.Remove(bus);
            return Save();
        }

        public bool Save()
        {
            var saved = _contet.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Bus bus)
        {
            throw new NotImplementedException();
        }
    }
}
