

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    internal class EmployeeRepo : IEmployeeRepo
    {
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            await using var context = new AppDbcontext();
            try
            {
                await context.Employees.AddAsync(employee);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            await using var context = new AppDbcontext();
            try
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

                if (employee is null) return false;

                employee.Delete("system");
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log ex
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(int id, string name)
        {
            await using var context = new AppDbcontext();
            try
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

                if (employee is null) return false;

                employee.SetName(name, "system");
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<bool> ToggleStateAsync(int id)
        {
            await using var context = new AppDbcontext();
            try
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee is null) return false;

                employee.ToggleActive("system");
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            await using var context = new AppDbcontext();
            try
            {
                return await context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            }
            catch (Exception ex)
            {
                // Log ex
                return null;
            }
        }

        public async Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter = null)
        {
            await using var context = new AppDbcontext();
            try
            {
                var query = context.Employees.Where(e => !e.IsDeleted);

                if (filter is not null)
                    query = query.Where(filter);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
               
                return new List<Employee>();
            }
        }
    }
}