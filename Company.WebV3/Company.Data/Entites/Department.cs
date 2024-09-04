using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entites
{
    public class Department : BaseEntity
    {
        public string Name;
        public int Code { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
