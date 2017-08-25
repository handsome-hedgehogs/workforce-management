using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    // Model to build DB Table
    // Includes DepartmentID Primary Key, Name and Collection of Employees
    public class Department
    {
        // PK
        [Key]   
        public int DepartmentId { get; set; }

        // Name of the Department
        [Required]
        public string Name { get; set; }

        // Collection of Employees to list all employees for each department
        public ICollection<Employee> Employees { get; set; }
    }
}
