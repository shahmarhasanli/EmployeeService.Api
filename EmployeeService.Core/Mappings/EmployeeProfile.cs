using AutoMapper;
using EmployeeService.Core.Dtos;
using EmployeeService.Core.Models;


namespace EmployeeService.Core.Mappings
{
    internal class EmployeeProfile
        : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeDetailDto,Employee>().ReverseMap();
        }
    }
}
