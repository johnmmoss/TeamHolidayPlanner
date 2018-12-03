using System.ComponentModel.DataAnnotations;

namespace TeamHolidayPlanner.Web.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }
    }
}
