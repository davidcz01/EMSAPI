using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmsAPIV2.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public int EmployeeCount { get; set; }

       

        public virtual List<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new List<Employee>();
            UpdateEmployeeCount();
        }

        public void UpdateEmployeeCount()
        {
            EmployeeCount = Employees?.Count ?? 0;
        }



    }
}
