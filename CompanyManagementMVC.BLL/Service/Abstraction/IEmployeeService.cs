using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.ModelVM.Response;

namespace CompanyManagementMVC.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Task<Response<List<GetEmployeeVM>>> GetActiveEmployeesAsync();
        Task<Response<GetEmployeeVM?>> GetEmployeeByIdAsync(int id);
        Task<Response<bool>> AddEmployeeAsync(CreateEmployeeVM model);
        Task<Response<bool>> DeleteEmployeeAsync(int id);
        Task<Response<bool>> UpdateEmployeeAsync(int id, string name, decimal salary);
        Task<Response<bool>> ToggleStateAsync(int id);
    }
}