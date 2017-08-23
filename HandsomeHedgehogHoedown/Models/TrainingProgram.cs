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
        [Display(Name="Trainging Program ID")]
        public int TrainingProgramId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name="End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name="Maximum Capacity")]
        public int MaxCapacity { get; set; }

        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
