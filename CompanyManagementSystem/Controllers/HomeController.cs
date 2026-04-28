using CompanyManagementMVC.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public HomeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetActiveEmployeesAsync();
            var departments = await _departmentService.GetAllDepartmentsAsync();

            ViewBag.EmployeeCount = employees.Data?.Count ?? 0;
            ViewBag.DepartmentCount = departments.Data?.Count ?? 0;
            ViewBag.ActiveEmployees = employees.Data?.Count(e => !e.IsDeleted) ?? 0;
            ViewBag.TotalSalaries = employees.Data?.Sum(e => e.Salary).ToString("C") ?? "$0.00";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}