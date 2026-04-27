using CompanyManagementMVC.DAL.DataBase;
using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(AppDbcontext context) : base(context) { }

        // ✅ Soft Delete (مش Hard Delete)
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var department = await GetByIdAsync(id);
            if (department is null || department.IsDeleted)
                return false;

            department.IsDeleted = true;
            return await SaveChangesAsync() > 0;
        }
    }
}