using Comapny.Repository.Interfaces;
using Company.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comapny.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDBContext _context;

        public UnitOfWork(CompanyDBContext context)
        {
            _context = context;

            DepartmentRepository = new DepartmentRepository(context);

            EmployeeRepository = new EmployeeRepository(context);
        }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public int Complete()
           => _context.SaveChanges();

    }
}
