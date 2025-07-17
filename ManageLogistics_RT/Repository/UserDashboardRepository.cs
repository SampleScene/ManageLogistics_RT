 using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManageLogistics_RT.Repository
{
    public class UserDashboardRepository : IUserDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<List<PassReciept>> GetAllUserReceipts()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userID = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userReceipts =  _context.passReciepts.Where(u => u.AppUserId == userID).Include(p => p.Price);
            return userReceipts.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            //var user = await _context.appUsers.FirstOrDefaultAsync(i => i.UserId == id);
            var user = await _context.appUsers.Include(u => u.UserAuthData).FirstOrDefaultAsync(i => i.UserId == id);
            return user;
        }
        //GetByIdNoTracking

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.appUsers.AsNoTracking().Include(u => u.UserAuthData).FirstOrDefaultAsync(i => i.UserId == id);
        }

        public  bool Update(AppUser user)
        {
            _context.appUsers.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }



    }
}