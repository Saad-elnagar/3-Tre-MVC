using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Response;

namespace CompanyManagementMVC.BLL.Service.Abstraction
{
    public interface IDepartmentService
    {
        Task<Response<List<DepartmentVM>>> GetAllDepartmentsAsync();
        Task<Response<int>> AddDepartmentAsync(CreateDepartmentVM model);
        Task<Response<bool>> DeleteDepartmentAsync(int id);
        Task<Response<DepartmentVM?>> GetDepartmentByIdAsync(int id);
        Task<Response<int>> UpdateDepartmentAsync(UpdateDepartmentVM model);
    }
}