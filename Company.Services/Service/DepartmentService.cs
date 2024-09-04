using Comapny.Repository.Interfaces;
using Company.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Entites;

namespace Company.Services.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Department department)
        {
            var mappedDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreatedAt = DateTime.Now,
            };
            
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);

            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
           _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            var department = _unitOfWork.DepartmentRepository.GetAll();

            return department;
        }

        public Department GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if(department is null)
                return null;

            return department;
        }
        public void Update(Department department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
