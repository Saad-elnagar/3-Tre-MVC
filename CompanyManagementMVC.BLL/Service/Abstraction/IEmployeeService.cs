using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.ModelVM.Response;

namespace CompanyManagementMVC.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Task<Response<List<GetEmployeeVM>>> GetActiveEmployeesAsync(string? search = null);
        Task<Response<GetEmployeeVM?>> GetEmployeeByIdAsync(int id);
        Task<Response<bool>> AddEmployeeAsync(CreateEmployeeVM model);
        Task<Response<bool>> UpdateEmployeeAsync(UpdateEmployeeVM model);
        Task<Response<bool>> DeleteEmployeeAsync(int id);
        Task<Response<bool>> ToggleStateAsync(int id);
    }
}