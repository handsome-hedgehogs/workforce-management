using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int TrainingProgramId { get; set; }
        public Employee Employee { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}
