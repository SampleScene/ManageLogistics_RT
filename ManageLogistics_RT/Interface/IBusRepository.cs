using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.Interface
{
    public interface IBusRepository
    {
        Task<IEnumerable<Bus>> GetAll();
        Task<Bus> GetById(int id);
        Task<IEnumerable<Bus>> GetByTerminalId(int terminalId);
        Task<IEnumerable<Bus>> GetByModel(string model);
        bool Add(Bus bus);
        bool Update(Bus bus);
        bool Delete(Bus bus);
        bool Save();
    }
}
