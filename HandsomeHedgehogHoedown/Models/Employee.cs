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
        // Denotes Employee's FirstName, limited to 20 characters
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        // Denotes Employee's LastName, limited to 50 characters
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        //Denotes Employee StartDate, DateTime Type
        [Required]
        public DateTime StartDate { get; set; }
        // FK from Department Table
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        // Collection of EmployeeComputers 
        public ICollection<EmployeeComputer> EmployeeComputers;
        // Collection of EmployeeTraining
        public ICollection<EmployeeTraining> EmployeeTrainings;
    }
}
