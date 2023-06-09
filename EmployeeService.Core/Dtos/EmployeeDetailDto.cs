using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Dtos
{
    public class EmployeeDetailDto : EmployeeDto
    {
        public DepartmentDto? Department;
    }
}
