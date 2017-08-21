using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    public class EmployeeComputer
    {
        [Key]
        public int EmployeeComputerId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int ComputerId { get; set; }
        public Employee Employee { get; set; }
        public Computer Computer { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
