using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        public IActionResult Index() => RedirectToAction(nameof(GetAllDepartment));

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(new List<DepartmentVM>());
            }

            return View(result.Data);
        }

        // ✅ GET: Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAllDepartment));
            }

            return View(result.Data);
        }


        [HttpGet]
        public IActionResult CreateDepartment() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartmentPost(CreateDepartmentVM model)
        {
            if (!ModelState.IsValid)
                return View(model);  // ✅ يرجع لـ CreateDepartment.cshtml

            var result = await _departmentService.AddDepartmentAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);  // ✅ يظهر Error في الـ View
                return View(model);  // ✅ يفضل في نفس الصفحة
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAllDepartment));  // ✅ بس لما ينجح يروح لـ GetAll
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(GetAllDepartment));
            }

            var vm = new UpdateDepartmentVM
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description
            };

            return View(vm);
        }

        // ✅ POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(UpdateDepartmentVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _departmentService.UpdateDepartmentAsync(model);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(GetAllDepartment));
        }

        // ✅ POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);

            TempData[result.IsSuccess ? "Success" : "Error"] = result.Message;
            return RedirectToAction(nameof(GetAllDepartment));
        }
    }
}