using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamHolidayPlanner.Domain
{
    public class Employee  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [StringLength(8)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string MaritalStatus { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid EmploymentGrade")]
        public int EmploymentGradeID { get; set; }
        public EmploymentGrade EmploymentGrade { get; set; }

        public List<EmployeeHoliday> Holidays { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }


    }
}
