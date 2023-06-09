using EmployeeService.Core.Dtos;
using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Data.Repositories
{
    internal class EmployeeRepository : RepositoryBase<Employee>,IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
        }

        public async Task<List<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Set.Where(a=>a.IsActive && !a.Deleted)
                .Select(e=> 
                        new EmployeeDto()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Surname = e.Surname,
                            BirthDate = e.BirthDate,
                            DepartmentId = e.DepartmentId,
                            CreatedDateTime = e.CreatedDateTime,
                            Deleted = e.Deleted,
                            IsActive = e.IsActive,
                            UpdatedDateTime = e.UpdatedDateTime
                        })
                        .ToListAsync(cancellationToken);
        }

        public async Task<List<Employee>> GetFilteredAndPaginatedAsync(string search, int page, int pageSize,CancellationToken cancellationToken)
        {
            IQueryable<Employee> query = Set.Where(a=>!a.Deleted && a.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Name.Contains(search) || e.Surname.Contains(search));
            }

            int skip = (page - 1) * pageSize;
            return await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
        }

        public  Task<Employee> GetEmployeeDetailByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return  Set
                .Include(a=>a.Department)
                .Where(a=>a.Id.Equals(id))
                .FirstAsync();
        }

    }

}
