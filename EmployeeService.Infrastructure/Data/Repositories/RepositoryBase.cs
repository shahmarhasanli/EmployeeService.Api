using EmployeeService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Data.Repositories
{
    internal abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _dbContext;

        protected DbSet<TEntity> Set { get; }

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Set = _dbContext.Set<TEntity>();
        }


        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Set.AddAsync(entity,cancellationToken);
            return Task.CompletedTask;
        }


        public void Update(TEntity entity)
        {
            Set.Update(entity);
        }


    }
}
