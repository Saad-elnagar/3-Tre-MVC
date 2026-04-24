using CompanyManagementMVC.DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {

        public DepartmentRepo(AppDbcontext context) : base(context)
        {

        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dep = await _context.Departments.FindAsync(id);

                if (dep == null) return false;

                _context.Departments.Remove(dep);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }


        }

        public IQueryable<Department> GetAllDepartmentsAsIQueryable()
        {
            return _context.Departments.AsNoTracking();
        }

        public Task<bool> UpdateDepartmentAsync(Department department, string name, string description)
        {
            var dep = _context.Departments.Find(department.Id);
            if (dep == null) return Task.FromResult(false);

            dep.Name = name;
            dep.Description = description;

            return Task.FromResult(true);
        }



        Task<bool> IDepartmentRepo.AddAsync(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);

            }
        }
    }
}
