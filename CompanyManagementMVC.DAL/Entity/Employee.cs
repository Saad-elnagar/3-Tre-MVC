namespace CompanyManagementMVC.DAL.Entity
{
    public class Employee
    {
        protected Employee() { }

        public Employee(string name, decimal salary, string createdBy)
        {
            Name = name;
            Salary = salary;
            CreatedBy = createdBy;
            CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public decimal Salary { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public void SetName(string name, string updatedBy)
        {
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedOn = DateTime.UtcNow;
        }

        public void Delete(string deletedBy)
        {
            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedOn = DateTime.UtcNow;
        }

        public void ToggleActive(string updatedBy)
        {
            IsDeleted = !IsDeleted;
            UpdatedBy = updatedBy;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}