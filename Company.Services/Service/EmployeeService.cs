using Comapny.Repository.Interfaces;
using Company.Data.Entites;
using Company.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Employee employee)
        {
            var mapemployee = new Employee
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                CreatedAt = DateTime.Now,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                ImageUrl = employee.ImageUrl,
            };
            _unitOfWork.EmployeeRepository.Add(mapemployee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        {
            var employee = _unitOfWork.EmployeeRepository.GetAll();

            return employee;
        }

        public Employee GetById(int? id)
        {
            if (id is null)
                return null;

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employee is null)
                return null;

            return employee;
        }

        public void Update(Employee employee)
        {

            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
