using Comapny.Repository.Interfaces;
using Company.Data.Entites;
using Company.Services.Dto;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string SearchInp)
        {
            IEnumerable<EmployeeDto> employee = new List<EmployeeDto>();

            if (string.IsNullOrEmpty(SearchInp))
            {
                employee = _employeeService.GetAll();
            }
            else
            {
                employee = _employeeService.GetByName(SearchInp);
            }
                return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var department = _departmentService.GetAll();

            ViewBag.Department = department;

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            if (ModelState.IsValid) 
            {
                _employeeService.Add(employee);

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id,string ViewName = "Details") 
        {

            var employee = _employeeService.GetById(id);

            if (employee == null) 
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var department = _departmentService.GetAll();

            ViewBag.Department = department;

            return Details(id,"Update");
        }

        [HttpPost]
        public IActionResult Update(int? id,EmployeeDto employee)
        {
            if(employee.Id != id.Value)
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }
            _employeeService.Update(employee);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id) 
        {
            var employee = _employeeService.GetById(id);

            if(employee == null)
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }
           _employeeService.Delete(employee);

            return RedirectToAction(nameof(Index));
        }
    }
}
