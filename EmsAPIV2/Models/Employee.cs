using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmsAPIV2.Models
{
    [Table("Employees")]
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int HolidayEntitlement { get; set; }

        [ForeignKey("DepartmentID")]  // Use the foreign key as DepartmentID
        public int DepartmentID { get; set; }
        public virtual Department? Department { get; set; }
    }


}

