using Comapny.Repository.Interfaces;
using Company.Data.Entites;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Company.web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var department = _departmentService.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public IActionResult Details(int? id,string viewName = "Details")
        {
            var department = _departmentService.GetById(id);

            if (department == null) 
            {
                return RedirectToAction("NotFoundPage",null,"Home");
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int? id,Department department)
        {
            if(department.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");

            _departmentService.Update(department);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }   
            _departmentService.Delete(department);

            return RedirectToAction(nameof(Index));
        }
    }
}
