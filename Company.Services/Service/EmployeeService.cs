using AutoMapper;
using Comapny.Repository.Interfaces;
using Company.Data.Entites;
using Company.Services.Dto;
using Company.Services.Helper;
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
		private readonly IMapper _mapper;

		public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public void Add(EmployeeDto employeeDto)
        {
            //Manual Mapping
            //var mapemployee = new Employee
            //{
            //    Name = employeeDto.Name,
            //    Age = employeeDto.Age,
            //    Address = employeeDto.Address,
            //    CreatedAt = DateTime.Now,
            //    HiringDate = employeeDto.HiringDate,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //    ImageUrl = employeeDto.ImageUrl,
            //};

            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");

            Employee mapemployee = _mapper.Map<Employee>(employeeDto);


			_unitOfWork.EmployeeRepository.Add(mapemployee);

            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {

            Employee employee = _mapper.Map<Employee>(employeeDto);

			_unitOfWork.EmployeeRepository.Delete(employee);

            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employee = _unitOfWork.EmployeeRepository.GetAll();

            IEnumerable<EmployeeDto> employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employee);

            return employeeDto;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
                return null;

            var employee =  _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employee is null)
                return null;

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetByName(string name)
        {
			var employees = _unitOfWork.EmployeeRepository.GetByName(name);

            IEnumerable<EmployeeDto> employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeeDto;

        }

        public void Update(EmployeeDto employeeDto)
        {
			Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.EmployeeRepository.Update(employee);

            _unitOfWork.Complete();
        }
    }
}
