using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{ 
    public interface IDepartmentRepo : IGenericRepo<Department>
    {
        Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>>? filter = null);
        Task<Department?> GetByIdAsync(int id);
        Task<bool> AddAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department, string name, string description);
        Task<bool> DeleteAsync(int id);
        IQueryable<Department> GetAllDepartmentsAsIQueryable();
    }
}
