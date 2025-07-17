using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.Interface
{
    public interface IEmployeeRepository
    {
        
        Task<Employee> GetEmployeeById(string id);
        Task<Employee> GetEmployeeByTerminalId(int terminalId);
        Task<Employee> GetByIdNoTracking(string id);
        bool Update(Employee employee);
        bool Save();
    }
}
