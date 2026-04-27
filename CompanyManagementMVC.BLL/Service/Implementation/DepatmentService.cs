using AutoMapper;
using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Response;
using CompanyManagementMVC.BLL.Service.Abstraction;
using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.BLL.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepo departmentRepo, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<Response<List<DepartmentVM>>> GetAllDepartmentsAsync()
        {
            try
            {
                var spec = new DepartmentSpecification();           
                var departments = await _departmentRepo.GetAllAsync(spec);
                var mapped = _mapper.Map<List<DepartmentVM>>(departments);  

                return Response<List<DepartmentVM>>.Success(mapped, "Departments retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<DepartmentVM>>.Failure(ex.Message);
            }
        }

        public async Task<Response<int>> AddDepartmentAsync(CreateDepartmentVM model)
        {
            try
            {
                var department = _mapper.Map<Department>(model);    
                department.IsDeleted = false;

                await _departmentRepo.AddAsync(department);
                var result = await _departmentRepo.SaveChangesAsync();

                return result > 0
                    ? Response<int>.Success(department.Id, "Department added successfully.")
                    : Response<int>.Failure("Failed to add department.");
            }
            catch (Exception ex)
            {
                return Response<int>.Failure(ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteDepartmentAsync(int id)
        {
            try
            {
                var department = await _departmentRepo.GetByIdAsync(id);
                if (department is null)
                    return Response<bool>.Failure($"Department with ID {id} not found.");

                _departmentRepo.Delete(department);
                var result = await _departmentRepo.SaveChangesAsync();

                return result > 0
                    ? Response<bool>.Success(true, "Department deleted successfully.")
                    : Response<bool>.Failure("Failed to delete department.");
            }
            catch (Exception ex)
            {
                return Response<bool>.Failure(ex.Message);
            }
        }

        public async Task<Response<DepartmentVM?>> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var spec = new DepartmentSpecification(id);         
                var department = await _departmentRepo.GetBySpecAsync(spec);

                if (department is null)
                    return Response<DepartmentVM?>.Failure($"Department with ID {id} not found.");

                var mapped = _mapper.Map<DepartmentVM>(department); 

                return Response<DepartmentVM?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Response<DepartmentVM?>.Failure(ex.Message);
            }
        }

        public async Task<Response<int>> UpdateDepartmentAsync(UpdateDepartmentVM model)
        {
            try
            {
                var department = await _departmentRepo.GetByIdAsync(model.Id);
                if (department is null)
                    return Response<int>.Failure($"Department with ID {model.Id} not found.");

                _mapper.Map(model, department);                    

                _departmentRepo.Update(department);
                var result = await _departmentRepo.SaveChangesAsync();

                return result > 0
                    ? Response<int>.Success(department.Id, "Department updated successfully.")
                    : Response<int>.Failure("Failed to update department.");
            }
            catch (Exception ex)
            {
                return Response<int>.Failure(ex.Message);
            }
        }
    }
}