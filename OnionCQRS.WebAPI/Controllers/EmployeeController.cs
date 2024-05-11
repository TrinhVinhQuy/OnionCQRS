using Microsoft.AspNetCore.Mvc;
using OnionCQRS.Application.Features.EmployeeFeatures.Queries;
using OnionCQRS.Application.Features.ProductFeatures.Queries;

namespace OnionCQRS.WebAPI.Controllers
{
    public class EmployeeController : BaseApiController
    {
        /// <summary>
        /// Gets all Employee.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllEmployDepartmentQuery()));
        }
    }
}
