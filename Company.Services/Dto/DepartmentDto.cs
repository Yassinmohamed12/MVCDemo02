using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public ICollection<EmployeeDto> EmployeesDto { get; set; } = new List<EmployeeDto>();

        public DateTime CreateAt { get; set; }
    }
}
