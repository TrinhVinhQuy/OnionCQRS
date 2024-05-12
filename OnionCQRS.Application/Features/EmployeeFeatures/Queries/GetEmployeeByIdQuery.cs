using AutoMapper;
using MediatR;
using OnionCQRS.Application.DTOs.Employee;
using OnionCQRS.Domain.Abstracts;

namespace OnionCQRS.Application.Features.EmployeeFeatures.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDetailDTO>
    {
        public int Id { get; set; }
        public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailDTO>
        {
            private readonly ISQLQueryHandler _sqlQueryHandler;
            private readonly IMapper _mapper;
            public GetEmployeeByIdHandler(ISQLQueryHandler sqlQueryHandler, IMapper mapper)
            {
                _sqlQueryHandler = sqlQueryHandler;
                _mapper = mapper;
            }
            public async Task<EmployeeDetailDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employees = await _sqlQueryHandler.ExecuteStoreProdecureReturnListAsync<EmployeeDTO>("GetAllEmployeeDepartment", null);

                var employee = employees.FirstOrDefault(x => x.EmployeeId == request.Id);

                if (employee == null)
                {
                    return null;
                }

                var result = _mapper.Map<EmployeeDetailDTO>(employee);
                return result;
            }
        }
    }
}
