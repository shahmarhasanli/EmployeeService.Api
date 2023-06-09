using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Extensions
{
    internal static class BaseEntityExtensions
    {
        public static void SetCreated(this BaseEntity entity,
            DateTime now)
        {
            entity.CreatedDateTime = now;
            entity.SetUpdated(now);
        }

        public static void SetDeleted(this BaseEntity entity,
            DateTime now)
        {
            entity.Deleted = true;
            entity.IsActive = false;
            entity.SetUpdated(now);
        }

        public static void SetUpdated(this BaseEntity entity,
            DateTime now)
        {
            entity.UpdatedDateTime = now;
        }
    }
}
