using Dapper;
using MediatR;
using OnionCQRS.Application.DTOs.Employee;
using OnionCQRS.Domain.Abstracts;
using System.Reflection.Metadata;

namespace OnionCQRS.Application.Features.EmployeeFeatures.Queries
{
    public class GetAllEmployDepartmentQuery : IRequest<IEnumerable<EmployeeDTO>>
    {
        public class GetAllEmployDepartmentQueryHandler : IRequestHandler<GetAllEmployDepartmentQuery, IEnumerable<EmployeeDTO>>
        {
            private readonly ISQLQueryHandler _sqlQueryHandler;
            public GetAllEmployDepartmentQueryHandler(ISQLQueryHandler sqlQueryHandler)
            {
                _sqlQueryHandler = sqlQueryHandler;
            }
            public async Task<IEnumerable<EmployeeDTO>> Handle(GetAllEmployDepartmentQuery request, CancellationToken cancellationToken)
            {
                var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnListAsync<EmployeeDTO>("GetAllEmployeeDepartment", null);
                return result;
            }
        }
    }
}
