using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Model Class to generate DB table for Employees
// Includes Employee ID Primary Key, First Name, Last Name, DepartmentID Foreign Key, 
// DateStart, instance of a Department, collection of Employee Computers, and a collection
// of Employee Training Programs
// Authored by : Jason Smith
namespace HandsomeHedgehogHoedown.Models
{
    public class Employee
    {
        // Primary Key
        [Key]
        public int EmployeeId { get; set; }

        // Denotes name character length limited to 20
        [Required]
        [StringLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        // Denotes name character length limited to 50
        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        // Foreign Key from Department Table
        [Required]
        public int DepartmentId { get; set; }

        // DateTime type, denotes start date of an employee
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name ="Start Date")]
        public DateTime DateStart { get; set; }

        // Instance of a Department
        public Department Department { get; set; }

        // Collection of EmployeeComputers relationships
        [Display(Name ="Computers")]
        public ICollection<EmployeeComputer> EmployeeComputers { get; set; }

        // Collection of EmployeeTraining relationships
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
