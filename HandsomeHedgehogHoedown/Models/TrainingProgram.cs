using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    // Model class to build DB table for Training Programs
    // Includes TrainingProgramID Primary Key, Name, StartDate, EndDate, MaxCapacity,
    // and a collection of Employee Training Programs
    public class TrainingProgram
    {
        // Primary Key
        [Key]
        [Display(Name="Trainging Program ID")]
        public int TrainingProgramId { get; set; }

        // Name of Training Program
        [Required]
        public string Name { get; set; }

        // DateTime type, denotes start date of the program
        // DataAnnotations for title format and date format
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }

        // DateTime type, denotes end date of the program
        // DataAnnotations for title format and date format
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        // Max Capacity of a training program
        [Required]
        [Display(Name="Maximum Capacity")]
        public int MaxCapacity { get; set; }

        // Collection from Joined Table EmployeeTrainings to list programs attending or have attended
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
