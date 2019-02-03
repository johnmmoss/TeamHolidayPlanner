using System;
using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Domain
{
    public class HolidayPeriod : IModificationHistory
    {
        [Key]
        public int HolidayPeriodID { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
