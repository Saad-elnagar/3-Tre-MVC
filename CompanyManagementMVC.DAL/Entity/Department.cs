namespace CompanyManagementMVC.DAL.Entity
{
    public class Department
    {
        protected Department() { }

        public Department(string name, string createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public void Update(string name, string updatedBy)
        {
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedOn = DateTime.Now;
        }

        public void Delete(string deletedBy)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedOn = DateTime.Now;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedBy = null;
            DeletedOn = null;
        }
   
        public ICollection<Employee> Employees { get; private set; }
            = new List<Employee>();
    }
}