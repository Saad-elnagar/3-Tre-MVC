
using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(AppDbcontext context) : base(context) {
           
        }

        public Task<bool> DeleteAsync(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null) return Task.FromResult(false);

            _context.Employees.Remove(emp);
            return Task.FromResult(true);
        }

        public async Task<bool> ToggleStateAsync(int id)
        {
            var emp = await GetByIdAsync(id);
            if (emp == null) return false;

            emp.IsActive = !emp.IsActive; 
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, string name, decimal salary)
        {
            var emp = await GetByIdAsync(id);
            if (emp == null) return false;

            emp.Name = name;
            emp.Salary = salary;
            Update(emp);
            await SaveChangesAsync();
            return true;
        }
    }
}