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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(CompanyDBContext context):base(context)
        {
        }
    }
}
