using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyManagementMVC.DAL.DataBase
{
    public class AppDbcontextFactory : IDesignTimeDbContextFactory<AppDbcontext>
    {
        public AppDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbcontext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=CompanyManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
            );

            return new AppDbcontext(optionsBuilder.Options);
        }
    }
}