namespace CompanyManagementMVC.DAL.DataBase
{
    public class AppDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=CompanyManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
            );
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}

