using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.PL.Models
{
    public class EmployeeEditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive.")]
        public decimal Salary { get; set; }
    }
}
