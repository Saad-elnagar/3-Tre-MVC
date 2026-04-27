namespace CompanyManagementMVC.DAL.Entity
{
    public class Employee : ModelBase
    {
        public Employee() { }

        public string Name { get; set; } = default!;
        public decimal Salary { get; set; }
        public Department? Department { get; set; } 
        public int? DepartmentId { get; set; }

    }
}