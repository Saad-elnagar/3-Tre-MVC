using CompanyManagementMVC.DAL.Specification.CompanyManagementMVC.DAL.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Specification
{
    public class DepartmentWithEmployeesSpecification : BaseSpecification<Department>
    {
        public DepartmentWithEmployeesSpecification(int id)
        {
            AddCriteria(d => d.Id == id);
            AddInclude(d => d.Employees!);  
        }
    }
}
