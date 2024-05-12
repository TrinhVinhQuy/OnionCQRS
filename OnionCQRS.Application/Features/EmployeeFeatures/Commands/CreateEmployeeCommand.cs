using AutoMapper;
using MediatR;
using OnionCQRS.Application.DTOs;
using OnionCQRS.Application.DTOs.Employee;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;
using System.Linq;

namespace OnionCQRS.Application.Features.EmployeeFeatures.Commands
{
    public class CreateEmployeeCommand : IRequest<ResponseAPI>
    {
        public CreateEmployeeDTO CreateEmployeeDTO { get; set; }
        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ResponseAPI>
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Employee> _employeeRepository;
            private readonly IRepository<Department> _departmentRepository;
            public CreateEmployeeCommandHandler(IMapper mapper,
                IRepository<Employee> employeeRepository,
                IRepository<Department> departmentRepository)
            {
                _mapper = mapper;
                _employeeRepository = employeeRepository;
                _departmentRepository = departmentRepository;
            }

            public async Task<ResponseAPI> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var result = new ResponseAPI();
                var department = await _departmentRepository.GetByIdAsync(request.CreateEmployeeDTO.DepartmentId);
                if (department == null)
                {
                    result.message = "DepartmentId not exist!";
                    result.success = false;
                    return result;
                }

                var employee = new Employee();
                _mapper.Map(request.CreateEmployeeDTO, employee);

                try
                {
                    var employeeNew = await _employeeRepository.InsertAsync(employee);
                    result.success = true;
                    result.message = "Create successful employee!";
                    result.data = new Employee
                    {
                        Id = employee.Id,
                        FullName = employee.FullName,
                        Birthday = employee.Birthday,
                        DepartmentId = employee.DepartmentId,
                        Email = employee.Email,
                        Phone = employee.Phone,
                    };
                }
                catch (Exception ex)
                {
                    result.success = true;
                    result.message = "Fail: " + ex.Message;
                }

                return result;
            }
        }
    }
}
