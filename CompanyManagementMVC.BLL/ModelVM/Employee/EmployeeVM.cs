using System.ComponentModel.DataAnnotations;

namespace CompanyManagementMVC.BLL.ModelVM.Employee
{
    public class GetEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; }
        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
    }

   
    
        public class CreateEmployeeVM
        {
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; } = default!;

            [Required(ErrorMessage = "Salary is required")]
            [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive")]
            public decimal Salary { get; set; }

            [Required(ErrorMessage = "Department is required")]  // ✅
            public int DepartmentId { get; set; }

            [Required(ErrorMessage = "CreatedBy is required")]
            public string CreatedBy { get; set; } = default!;
        }
   
    public class UpdateEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}