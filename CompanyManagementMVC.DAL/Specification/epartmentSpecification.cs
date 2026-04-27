using CompanyManagementMVC.DAL.Entity;
using CompanyManagementMVC.DAL.Specification.CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.DAL.Specification
{
    public class DepartmentSpecification : BaseSpecification<Department>
    {
        public DepartmentSpecification()
        {
            AddCriteria(d => !d.IsDeleted);
            AddOrderBy(d => d.Name);
        }

        public DepartmentSpecification(int id) : this()
        {
            AddCriteria(d => d.Id == id);
        }
    }
}