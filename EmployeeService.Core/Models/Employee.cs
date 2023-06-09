using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Models
{
    public class Employee : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual Department? Department { get; set; }

    }
}
