﻿namespace TeamHolidayPlanner.Domain
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}
