using System;
using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class EmployeeHoliday
    {
        [Key]
        public int EmployeeHolidayId { get; set; }

        [Required]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public int HolidayPeriodID { get; set; }
        public HolidayPeriod HolidayPeriod { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool? Authorised { get; set; }

        public DateTime? AuthorisedDate { get; set; }

        public string AuthorisedBy { get; set; }
    }
}
