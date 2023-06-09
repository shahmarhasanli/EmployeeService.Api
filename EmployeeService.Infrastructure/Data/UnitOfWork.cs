using EmployeeService.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Infrastructure.Data.Repositories;

namespace EmployeeService.Infrastructure.Data
{
    internal class UnitOfWork
        : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IDbContextTransaction? _transaction;

        private IEmployeeRepository _employees;
        private IDepartmentRepository _departments;
        public UnitOfWork(
            AppDbContext context)
        {
            _context = context;
        }
        public IDepartmentRepository Departments =>
            _departments ??= new DepartmentRepository(_context);

        public IEmployeeRepository Employees =>
            _employees ??= new EmployeeRepository(_context);
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
