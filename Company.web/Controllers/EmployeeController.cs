using Comapny.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll();

            return View(employee);
        }
        public IActionResult Details() 
        {
            var employee = _employeeRepository.GetAll();

            return View(employee);
        }
        public IActionResult Delete() 
        {
            var employee = _employeeRepository.Delete;

            return View(employee);
        }
    }
}
