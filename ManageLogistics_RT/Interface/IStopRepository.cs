using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.Interface
{
    public interface IStopRepository
    {
        Task<IEnumerable<Stop>> GetAll();
        Task<Stop> GetByIdAsync(int id);
        Task<Stop> GetByIdAsyncNoTracking(int id);
        bool Add(Stop stop);
        bool Update(Stop stop);
        bool Delete(Stop stop);
        bool Save();
    }
}
