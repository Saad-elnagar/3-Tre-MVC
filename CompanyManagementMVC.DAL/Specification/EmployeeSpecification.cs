using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Specification.CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.DAL.Specification
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        // ✅ Active Employees + Include Department
        public EmployeeSpecification()
        {
            AddCriteria(e => !e.IsDeleted);
            AddInclude(e => e.Department!);
            AddOrderBy(e => e.Name);
        }

        // ✅ Search by Name
        public EmployeeSpecification(string search) : this()
        {
            if (!string.IsNullOrEmpty(search))
                AddCriteria(e => e.Name.Contains(search));
        }

        // ✅ Get By Id + Include Department
        public EmployeeSpecification(int id) : this()
        {
            AddCriteria(e => e.Id == id);
        }

        // ✅ Pagination
        public EmployeeSpecification(int skip, int take, string? search = null) : this(search)
        {
            ApplyPaging(skip, take);
        }
    }
}