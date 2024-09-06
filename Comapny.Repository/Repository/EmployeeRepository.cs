using Comapny.Repository.Interfaces;
using Company.Data.Context;
using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comapny.Repository.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDBContext _context;
        public EmployeeRepository(CompanyDBContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetByName(string name)
            =>_context.Employees.Where(e => e.Name.Trim().ToLower().Contains(name.Trim().ToLower())).ToList();
    }
}
