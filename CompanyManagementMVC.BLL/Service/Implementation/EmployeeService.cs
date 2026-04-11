using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.ModelVM.Response;
using CompanyManagementMVC.BLL.Service.Abstraction;
using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Repo.Abstraction;

namespace CompanyManagementMVC.BLL.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Response<List<GetEmployeeVM>>> GetActiveEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepo.GetAllAsync(e => !e.IsDeleted);

                var mapped = employees.Select(e => new GetEmployeeVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Salary = e.Salary,
                    IsDeleted = e.IsDeleted
                }).ToList();

                return Response<List<GetEmployeeVM>>.Success(mapped, "Active employees retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetEmployeeVM>>.Failure($"Failed to retrieve employees: {ex.Message}");
            }
        }

        public async Task<Response<GetEmployeeVM?>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepo.GetByIdAsync(id);

                if (employee is null)
                    return Response<GetEmployeeVM?>.Failure($"Employee with ID {id} not found.");

                var mapped = new GetEmployeeVM
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    IsDeleted = employee.IsDeleted
                };

                return Response<GetEmployeeVM?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Response<GetEmployeeVM?>.Failure($"Failed to retrieve employee: {ex.Message}");
            }
        }

        public async Task<Response<bool>> AddEmployeeAsync(CreateEmployeeVM model)
        {
            try
            {
                var employee = new Employee(model.Name, model.Salary, model.CreatedBy);
                var result = await _employeeRepo.AddEmployeeAsync(employee);

                return result
                    ? Response<bool>.Success(true, "Employee added successfully.")
                    : Response<bool>.Failure("Failed to add employee.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure($"Failed to add employee: {ex.Message}");
            }
        }

        public async Task<Response<bool>> DeleteEmployeeAsync(int id)
        {
            try
            {
                var result = await _employeeRepo.DeleteEmployeeAsync(id);

                return result
                    ? Response<bool>.Success(true, "Employee deleted successfully.")
                    : Response<bool>.Failure($"Employee with ID {id} not found or already deleted.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure($"Failed to delete employee: {ex.Message}");
            }
        }

        public async Task<Response<bool>> UpdateEmployeeAsync(int id, string name, decimal salary)
        {
            try
            {
                var result = await _employeeRepo.UpdateEmployeeAsync(id, name, salary);

                return result
                    ? Response<bool>.Success(true, "Employee updated successfully.")
                    : Response<bool>.Failure($"Employee with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure($"Failed to update employee: {ex.Message}");
            }
        }

        public async Task<Response<bool>> ToggleStateAsync(int id)
        {
            try
            {
                var result = await _employeeRepo.ToggleStateAsync(id);

                return result
                    ? Response<bool>.Success(true, "Employee state toggled.")
                    : Response<bool>.Failure($"Employee with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure($"Failed to toggle employee state: {ex.Message}");
            }
        }
    }
}