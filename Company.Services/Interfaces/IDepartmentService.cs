using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comapny.Repository;
using Company.Services.Dto;

namespace Company.Services.Interfaces
{
    public interface IDepartmentService
    {
        public DepartmentDto GetById(int? id);

        public IEnumerable<DepartmentDto> GetAll();

        public void Add(DepartmentDto department);

        public void Update(DepartmentDto department);

        public void Delete(DepartmentDto department);
    }
}
