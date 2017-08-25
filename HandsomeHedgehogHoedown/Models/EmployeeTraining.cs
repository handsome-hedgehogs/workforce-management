using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    // Model class to build join DB table for Training Programs and Employees
    // Includes EmployeeTrainingID Primary Key, Foreign Key EmployeeID, Training Program ID, an instance
    // of an Employee, and an instance of a Training Program
    public class EmployeeTraining
    {
        // Primary Key
        [Key]
        public int EmployeeTrainingId { get; set; }

        // Foreign Key from Employee table
        [Required]
        public int EmployeeId { get; set; }

        // Foreign Key from Training Program table
        [Required]
        public int TrainingProgramId { get; set; }

        // Instance of an Employee
        public Employee Employee { get; set; }

        // Instance of a Training Program
        public TrainingProgram TrainingProgram { get; set; }
    }
}
