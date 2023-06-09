using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Models
{
    public abstract class BaseEntity
    {
        public Guid? CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
    }
}
