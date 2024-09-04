using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Employee GetById(int? id);

        public IEnumerable<Employee> GetAll();

        public void Add(Employee employee);

        public void Update(Employee employee);

        public void Delete(Employee employee);
    }
}
