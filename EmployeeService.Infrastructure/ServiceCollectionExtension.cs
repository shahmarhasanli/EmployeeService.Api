using EmployeeService.Core.Interfaces;
using EmployeeService.Infrastructure.Data;
using EmployeeService.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EmployeeService.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                    //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    options.UseInMemoryDatabase("ems_db")
                    )
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmployeeRepository,EmployeeRepository>()
                .AddScoped<IDepartmentRepository,DepartmentRepository>();

            return services;
        }
    }
}
