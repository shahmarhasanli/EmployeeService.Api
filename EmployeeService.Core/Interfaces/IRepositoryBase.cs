using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);

    }
}
