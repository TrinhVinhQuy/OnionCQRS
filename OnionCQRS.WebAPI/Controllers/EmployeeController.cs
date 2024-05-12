using Microsoft.AspNetCore.Mvc;
using OnionCQRS.Application.DTOs;
using OnionCQRS.Application.Features.EmployeeFeatures.Commands;
using OnionCQRS.Application.Features.EmployeeFeatures.Queries;
using OnionCQRS.Application.Features.ProductFeatures.Commands;
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
        /// <summary>
        /// Gets Employee Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }
        /// <summary>
        /// Creates a New Employee.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            // Call your Mediator to handle the creation logic
            return  Ok(await Mediator.Send(command));
        }
    }
}
