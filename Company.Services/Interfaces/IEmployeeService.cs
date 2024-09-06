using Company.Data.Entites;
using Company.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IEmployeeService
    {
        public EmployeeDto GetById(int? id);

        public IEnumerable<EmployeeDto> GetAll();

        public void Add(EmployeeDto employee);

        public void Update(EmployeeDto employee);

        public void Delete(EmployeeDto employee);

        public IEnumerable<EmployeeDto> GetByName(string name);

    }
}
