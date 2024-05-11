using OnionCQRS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCQRS.Domain.Entities
{
    public class Department: BaseEntity
    {
        public string DepartmentName { get; set; }
        public string Phone { get; set;}
        public string Describe { get; set;}
        public ICollection<Employee> employees { get; set;}
    }
}
