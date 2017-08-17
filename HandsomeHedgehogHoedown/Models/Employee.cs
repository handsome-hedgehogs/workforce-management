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
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        // FK from Department Table
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        // Collection of EmployeeComputers relationships
        public ICollection<EmployeeComputer> EmployeeComputers;
        // Collection of EmployeeTraining relationships
        public ICollection<EmployeeTraining> EmployeeTrainings;
    }
}
