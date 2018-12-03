using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
