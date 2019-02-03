using System.Collections.Generic;

namespace TeamHolidayPlanner.Domain
{
    public class Role
    {
        public int RoleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
