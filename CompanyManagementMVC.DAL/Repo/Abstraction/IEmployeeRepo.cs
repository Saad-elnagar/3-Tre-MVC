using System.Linq.Expressions;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, string name);
        Task<bool> ToggleStateAsync(int id);
        Task<Employee?> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter = null);
    }
}