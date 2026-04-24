using CompanyManagementMVC.BLL.Service.Abstraction;
using CompanyManagementMVC.BLL.Service.Implementation;
using CompanyManagementMVC.DAL.DataBase;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using CompanyManagementMVC.DAL.Repo.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. DbContext
            builder.Services.AddDbContext<AppDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Repositories
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            // 3. Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            // 4. Controllers & Views
            builder.Services.AddControllersWithViews();

            // ✅ 5. Build AFTER all registrations
            var app = builder.Build();

            // ✅ 6. Auto-Migrate هنا بعد ما الـ App تبني
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbcontext>();
                    context.Database.Migrate(); // بتطبق أي Pending Migrations
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // 7. Middleware Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}