using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    // Model class to build join DB table for Computers and Employees
    // Includes EmployeeComputerID Primary Key, EmployeeID Foreign Key, ComputerID Foreign Key,
    // an instance of an Employee, an instance of a Computer, StartDate, and EndDate
    public class EmployeeComputer
    {
        // Primary Key
        [Key]
        public int EmployeeComputerId { get; set; }

        // Foreign Key from Employee table
        [Required]
        public int EmployeeId { get; set; }

        // Foreign Key from Computer table
        [Required]
        public int ComputerId { get; set; }

        // Instance of an Employee
        public Employee Employee { get; set; }

        // Instance of a Computer
        public Computer Computer { get; set; }

        // Start Date of when the computer is assigned to an employee
        [Required]
        public DateTime StartDate { get; set; }

        // End Date of when the computer is unassigned to an employee
        public DateTime? EndDate { get; set; }
    }
}
