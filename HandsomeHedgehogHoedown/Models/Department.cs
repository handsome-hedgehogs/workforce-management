using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    public class Department
    {
        [Key]   
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
