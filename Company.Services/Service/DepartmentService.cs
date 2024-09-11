using Comapny.Repository.Interfaces;
using Company.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Entites;
using Company.Services.Dto;
using Company.Services.Mapping;
using AutoMapper;

namespace Company.Services.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DepartmentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public void Add(DepartmentDto departmentDto)
        {
            //var mappedDepartment = new Department
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreatedAt = DateTime.Now,
            //};

            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.DepartmentRepository.Add(department);

            _unitOfWork.Complete();
        }

        public void Delete(int? Id)
        {
            //Department department = _mapper.Map<Department>(departmentDto);

            var department = _unitOfWork.DepartmentRepository.GetById(Id.Value);

           _unitOfWork.DepartmentRepository.Delete(department);

            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var department = _unitOfWork.DepartmentRepository.GetAll();

            IEnumerable<DepartmentDto> departmentDto = _mapper.Map<IEnumerable<DepartmentDto>>(department);

            return departmentDto;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if(department is null)
                return null;

            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);

            return departmentDto;
        }
        public void Update(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.DepartmentRepository.Update(department);

            _unitOfWork.Complete();
        }
    }
}
