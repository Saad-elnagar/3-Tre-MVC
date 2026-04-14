using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{ 
    public interface IDepartmentRepo
    {
        Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>>? filter = null);
        Task<Department?> GetByIdAsync(int id);
        Task<bool> AddAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}
