using AutoMapper;
using EmployeeService.Core.Dtos;
using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Mappings
{
    internal class DepartmentProfile
        : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>()
              .ReverseMap();
        }
    }
}
