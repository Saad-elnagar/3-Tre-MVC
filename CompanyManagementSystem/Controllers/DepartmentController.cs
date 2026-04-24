using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.DAL.Entity;
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

        [HttpGet]
        public async Task <IActionResult> GetAllDepartment()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }



        [HttpGet]
        public IActionResult CreateDepatment()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> CreateDepatmentPost(CreateDepartmentVM department)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.AddDepartmentAsync(department);

                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.Message;
                    return View(department);
                }


                TempData["Success"] = result.Message;
                return RedirectToAction(nameof(GetAllDepartment));
            }
            return View(department);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
