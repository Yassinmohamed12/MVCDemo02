using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comapny.Repository.Interfaces
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        public Employee GetByName(string name);


    }
}
