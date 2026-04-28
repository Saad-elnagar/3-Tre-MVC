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
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetActiveEmployeesAsync();

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];

            return View(result.Data ?? new List<GetEmployeeVM>());
        }

        // ====== GET: Create Form + Dropdown ======
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillDepartmentsDropdown();
            return View();
        }

        // ====== POST: Create ======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeVM model)
        {
            // ✅ 1. لو فيه Validation Error → اعرضهم + املأ Dropdown تاني
            if (!ModelState.IsValid)
            {
                await FillDepartmentsDropdown();
                return View(model);
            }

            // ✅ 2. نادي الـ Service
            var result = await _employeeService.AddEmployeeAsync(model);

            // ✅ 3. لو فشل من الـ Service → اعرض Error + املأ Dropdown تاني
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                await FillDepartmentsDropdown();
                return View(model);
            }

            // ✅ 4. نجح → روح لـ GetAll
            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ====== GET: Edit ======
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAll));
            }

            var vm = new UpdateEmployeeVM
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Salary = result.Data.Salary,
                DepartmentId = result.Data.DepartmentId ?? 0
            };

            await FillDepartmentsDropdown();
            return View(vm);
        }

        // ====== POST: Edit ======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                await FillDepartmentsDropdown();
                return View(model);
            }

            var result = await _employeeService.UpdateEmployeeAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                await FillDepartmentsDropdown();
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ====== POST: Delete ======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

        // ====== Helper: يملى Dropdown ======
        private async Task FillDepartmentsDropdown()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(result.Data ?? new List<DepartmentVM>(), "Id", "Name");
        }
    }
}