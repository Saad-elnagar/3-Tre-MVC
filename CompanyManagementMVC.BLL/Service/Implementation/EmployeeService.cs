using AutoMapper;
using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.ModelVM.Response;
using CompanyManagementMVC.BLL.Service.Abstraction;
using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.BLL.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<Response<List<GetEmployeeVM>>> GetActiveEmployeesAsync(string? search = null)
        {
            try
            {
                var spec = string.IsNullOrEmpty(search)
                    ? new EmployeeSpecification()
                    : new EmployeeSpecification(search);

                var employees = await _employeeRepo.GetAllAsync(spec);
                var mapped = _mapper.Map<List<GetEmployeeVM>>(employees);

                return Response<List<GetEmployeeVM>>.Success(mapped, "Employees retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetEmployeeVM>>.Failure(ex.Message);
            }
        }

        public async Task<Response<GetEmployeeVM?>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var spec = new EmployeeSpecification(id);
                var employee = await _employeeRepo.GetBySpecAsync(spec);

                if (employee is null)
                    return Response<GetEmployeeVM?>.Failure($"Employee with ID {id} not found.");

                var mapped = _mapper.Map<GetEmployeeVM>(employee);
                return Response<GetEmployeeVM?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Response<GetEmployeeVM?>.Failure(ex.Message);
            }
        }

        public async Task<Response<bool>> AddEmployeeAsync(CreateEmployeeVM model)
        {
            try
            {
                var employee = _mapper.Map<Employee>(model);
                employee.IsDeleted = false;

                await _employeeRepo.AddAsync(employee);
                var result = await _employeeRepo.SaveChangesAsync();

                return result > 0
                    ? Response<bool>.Success(true, "Employee added successfully.")
                    : Response<bool>.Failure("Failed to add employee.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure(ex.Message);
            }
        }

        public async Task<Response<bool>> UpdateEmployeeAsync(UpdateEmployeeVM model)
        {
            try
            {
                var employee = await _employeeRepo.GetByIdAsync(model.Id);
                if (employee is null)
                    return Response<bool>.Failure($"Employee with ID {model.Id} not found.");

                _mapper.Map(model, employee);  // ✅ بيعمل Update لكل الـ Properties

                _employeeRepo.Update(employee);
                var result = await _employeeRepo.SaveChangesAsync();

                return result > 0
                    ? Response<bool>.Success(true, "Employee updated successfully.")
                    : Response<bool>.Failure("Failed to update employee.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure(ex.Message);
            }
        }
        public async Task<Response<bool>> DeleteEmployeeAsync(int id)
        {
            try
            {
                var result = await _employeeRepo.DeleteAsync(id);
                return result
                    ? Response<bool>.Success(true, "Employee deleted successfully.")
                    : Response<bool>.Failure($"Employee with ID {id} not found or already deleted.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure(ex.Message);
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
                return Response<bool>.Failure(ex.Message);
            }
        }

      
    }
}