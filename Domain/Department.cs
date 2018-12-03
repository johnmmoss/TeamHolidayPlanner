using System;
using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class Department : IModificationHistory
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
