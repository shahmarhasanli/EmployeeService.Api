using EmployeeService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces
{
    public interface IDepartmentService
    {
            Task<List<DepartmentDto>> GetAllDepartmentsAsync();
            Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
            Task<Guid> AddDepartmentAsync(DepartmentDto departmentDto);
            Task UpdateDepartmentAsync(Guid id,DepartmentDto departmentDto);
            Task DeleteDepartmentAsync(Guid id);
    }
}
