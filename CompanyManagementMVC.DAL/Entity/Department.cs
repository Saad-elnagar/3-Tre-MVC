namespace CompanyManagementMVC.DAL.Entity
{
    public class Department : ModelBase
    {
        public Department() { }

    
        public string Name { get;  set; }
        public string Description { get;  set; }
       
   
        public ICollection<Employee> Employees { get;  set; }
            = new List<Employee>();
    }
}