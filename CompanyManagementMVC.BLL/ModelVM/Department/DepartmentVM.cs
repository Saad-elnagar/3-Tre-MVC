using CompanyManagementMVC.BLL.ModelVM.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.BLL.ModelVM.Department
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<EmployeeInDeptVM>? Employees { get; set; }
    }
    public class EmployeeInDeptVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; }
    }


}
