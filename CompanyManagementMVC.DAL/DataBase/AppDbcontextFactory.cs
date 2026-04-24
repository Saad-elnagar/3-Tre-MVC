//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;


//namespace CompanyManagementMVC.DAL.DataBase
//{
//    public class AppDbcontextFactory : IDesignTimeDbContextFactory<AppDbcontext>
//    {
//        public AppDbcontext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<AppDbcontext>();

//            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CompanyManagementSystem");

//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(basePath)
//                .AddJsonFile("appsettings.json", optional: false)
//                .Build();

//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            optionsBuilder.UseSqlServer(connectionString);

//            return new AppDbcontext(optionsBuilder.Options);
//        }
//    }
//}