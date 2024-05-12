namespace OnionCQRS.Application.DTOs.Employee
{
    public class CreateEmployeeDTO
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
