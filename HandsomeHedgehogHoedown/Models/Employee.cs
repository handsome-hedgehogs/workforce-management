using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Model Class to generate DB table for Employees
// Authored by : Jason Smith
namespace HandsomeHedgehogHoedown.Models
{
    public class Employee
    {
        // PK 
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        // FK from Department Table
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name ="Start Date")]
        public DateTime DateStart { get; set; }
        public Department Department { get; set; }
        // Collection of EmployeeComputers relationships
        [Display(Name ="Computers")]
        public ICollection<EmployeeComputer> EmployeeComputers { get; set; }
        // Collection of EmployeeTraining relationships
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
