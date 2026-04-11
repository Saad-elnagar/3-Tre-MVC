using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

       
        public IActionResult Index() => RedirectToAction(nameof(GetAll));

        
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetActiveEmployeesAsync();

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }

       
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

        
        public IActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _employeeService.AddEmployeeAsync(model);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAll));
            }

            return View(result.Data);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError(nameof(name), "Name is required.");
                return View();
            }

            var result = await _employeeService.UpdateEmployeeAsync(id, name);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View();
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);

            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleState(int id)
        {
            var result = await _employeeService.ToggleStateAsync(id);

            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAll));
        }
    }
}