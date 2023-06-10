using EmployeeService.Core.Dtos;
using EmployeeService.Core.Models;


namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Employee>> GetFilteredAndPaginatedAsync(string search, int page, int pageSize,CancellationToken cancellationToken);
        Task<Employee> GetEmployeeDetailByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
