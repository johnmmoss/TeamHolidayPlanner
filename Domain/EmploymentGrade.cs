using System;
using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class EmploymentGrade : IModificationHistory
    {
        [Key]
        public int EmploymentGradeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public int AnnualLeaveEntitlement { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
