using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Data.Repositories
{
    internal class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await Set.Where(a=>a.IsActive && !a.Deleted).ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(Guid id)
        {
            return await Set.FirstOrDefaultAsync(a=>a.Id.Equals(id) && a.IsActive && !a.Deleted);
        }


    }
}
