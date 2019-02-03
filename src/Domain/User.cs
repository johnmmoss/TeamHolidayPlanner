using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public int? RoleID { get; set; }
        public Role Roles { get; set; }
    }
}
