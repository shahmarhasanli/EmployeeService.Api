using EmployeeService.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedById).IsRequired(false);
                entity.Property(e => e.CreatedDateTime).IsRequired();
                entity.Property(e => e.UpdatedById).IsRequired(false);
                entity.Property(e => e.UpdatedDateTime).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.Deleted).IsRequired();

                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId);
                    });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id).HasDefaultValueSql("(newid())");


                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.CreatedById).IsRequired(false);
                entity.Property(d => d.CreatedDateTime).IsRequired();
                entity.Property(d => d.UpdatedById).IsRequired(false);
                entity.Property(d => d.UpdatedDateTime).IsRequired();
                entity.Property(d => d.IsActive).IsRequired();
                entity.Property(d => d.Deleted).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
