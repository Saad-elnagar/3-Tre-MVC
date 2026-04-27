using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyManagementSystem.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;  // ✅ عشان الـ Dropdown

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public IActionResult Index() => RedirectToAction(nameof(GetAll));

        // ✅ GET: All Employees (with Department via Specification + AutoMapper)
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetActiveEmployeesAsync();

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(new List<GetEmployeeVM>());
            }

            return View(result.Data);
        }

        // ✅ GET: Details (with Department included via Spec)
        public async Task<IActionResult> Details(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAll));
            }

            return View(result.Data);
        }

        // ✅ GET: Create + Department Dropdown
        public async Task<IActionResult> Create()
        {
            await PopulateDepartmentsDropdown();
            return View();
        }

        // ✅ POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropdown();
                return View(model);
            }

            var result = await _employeeService.AddEmployeeAsync(model);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                await PopulateDepartmentsDropdown();
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ✅ GET: Edit + Department Dropdown
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAll));
            }

            // Map GetEmployeeVM → EmployeeVM
            var vm = new UpdateEmployeeVM
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Salary = result.Data.Salary,
                DepartmentId = result.Data.DepartmentId ?? 0  // لو عندك DepartmentId في GetEmployeeVM
            };

            await PopulateDepartmentsDropdown();
            return View(vm);
        }

        // ✅ POST: Edit (with Department)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsDropdown();
                return View(model);
            }

            var result = await _employeeService.UpdateEmployeeAsync(model);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                await PopulateDepartmentsDropdown();
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ✅ POST: Delete (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ✅ POST: Toggle State
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleState(int id)
        {
            var result = await _employeeService.ToggleStateAsync(id);
            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ✅ Helper: يملى الـ Dropdown
        private async Task PopulateDepartmentsDropdown()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(result.Data ?? new List<DepartmentVM>(), "Id", "Name");
        }
    }
}