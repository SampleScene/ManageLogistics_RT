using ManageLogistics_RT.Data.Enum;
using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.Interface
{
    public interface IPriceRepository
    {
        Task<IEnumerable<Price>> GetAll();
        Task<Price> GetByIdAsync(int id);
        Task<Price> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Price>> GetPriceByType(TypePrice type);

        bool Add(Price price);
        bool Update(Price price);
        bool Delete(Price price);
        bool Save();
    }
}
