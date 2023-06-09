using EmployeeService.Core.Dtos;
using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
        Task<List<EmployeeDto>> GetFilteredAndPaginatedEmployeesAsync(string search, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<EmployeeDetailDto> GetEmployeeDetailsByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task<EmployeeDetailDto> AddEmployeeAsync(EmployeeDto employee, CancellationToken cancellationToken = default);
        Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateEmployeeAsync(Guid id, EmployeeDto updatedEmployee, CancellationToken cancellationToken = default);
    }
}
