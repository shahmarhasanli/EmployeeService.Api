using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, Services.EmployeeService>()
                .AddScoped<IDepartmentService,DepartmentService>();
            return services;
        }
    }
}
