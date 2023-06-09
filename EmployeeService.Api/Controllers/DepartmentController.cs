using EmployeeService.Core.Dtos;
using EmployeeService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("all-departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("department/{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            return Ok(department);
        }
        [ProducesResponseType(typeof(Guid),StatusCodes.Status201Created)]
        [HttpPost("add-department")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            var addedDepartmentId = await _departmentService.AddDepartmentAsync(departmentDto);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = addedDepartmentId }, null);
        }

        [HttpDelete("delete-department/{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
