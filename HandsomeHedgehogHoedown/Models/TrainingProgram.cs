using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    public class TrainingProgram
    {
        [Key]
        public int TrainingProgramId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
