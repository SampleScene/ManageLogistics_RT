using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Employee> GetByIdNoTracking(string id)
        {
            return await _context.employees.AsNoTracking().Include(u => u.UserAuthData).FirstOrDefaultAsync(i => i.EmployeeId == id);
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            var user = await _context.employees.Include(u => u.UserAuthData).FirstOrDefaultAsync(i => i.EmployeeId == id);
            return user;
        }

        public async Task<Employee> GetEmployeeByTerminalId(int terminalId)
        {
            return await _context.employees.AsNoTracking().Include(u => u.UserAuthData).FirstOrDefaultAsync(i => i.TerminalId == terminalId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Employee employee)
        {
            _context.employees.Update(employee);
            return Save();
        }
    }
}
