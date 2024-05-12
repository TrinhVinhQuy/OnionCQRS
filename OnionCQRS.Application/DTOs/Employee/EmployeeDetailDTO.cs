using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCQRS.Application.DTOs.Employee
{
    public class EmployeeDetailDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime EmployeeBirthday { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
