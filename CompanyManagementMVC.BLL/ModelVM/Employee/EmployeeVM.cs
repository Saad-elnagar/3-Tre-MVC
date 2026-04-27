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
        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
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