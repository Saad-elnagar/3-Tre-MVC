using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.BLL.ModelVM.Department
{
    public class CreateDepartmentVM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        


    }
}
