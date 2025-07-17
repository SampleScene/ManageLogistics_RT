using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels.UserDashboard;

namespace ManageLogistics_RT.Interface
{
    public interface IUserDashboardRepository
    {
        Task<List<PassReciept>> GetAllUserReceipts();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}