using EmployeeService.Core.Dtos;
using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeService.Api.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("employees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("filtered-paginated-employees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetFilteredAndPaginatedEmployeesAsync([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var employees = await _employeeService.GetFilteredAndPaginatedEmployeesAsync(search, page, pageSize);
            return Ok(employees);
        }

        [ProducesResponseType(typeof(EmployeeDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("employee-detail/{id}",Name = "GetEmployeeDetailByIdAsync")]
        public async Task<IActionResult> GetEmployeeDetailByIdAsync(Guid id)
        {
            var employee = await _employeeService.GetEmployeeDetailsByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDto employee,CancellationToken cancellationToken = default)
        {
            await _employeeService.AddEmployeeAsync(employee,cancellationToken);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete-employee/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("update-employee/{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] EmployeeDto updatedEmployee,CancellationToken cancellationToken)
        {
            await _employeeService.UpdateEmployeeAsync(id,updatedEmployee, cancellationToken);
            return NoContent();
        }

    }
}
