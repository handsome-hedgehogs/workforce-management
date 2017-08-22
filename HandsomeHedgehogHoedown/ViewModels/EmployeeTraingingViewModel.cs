using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandsomeHedgehogHoedown.Models;

// Class that is a ViewModel for TrainingProgramController and TrainingProgram/Views/Details
// Creates instance of TrainingProgram Model
// Create List<Employee> to add Employee objects that match query in TrainingProgramController Detail Method
// Authored by : Tamela Lerma
namespace HandsomeHedgehogHoedown.ViewModels
{
    public class EmployeeTrainingViewModel
    {
        // Create instance of TrainingProgram Model to pass into Controller
        public TrainingProgram tp { get; set; }
        // Create List to add Employees that match query to
        public List<Employee> Employees {get; set;}
        // use constructor method to creat object instance of List<Employee>
        public EmployeeTrainingViewModel()
        {
            Employees = new List<Employee>();
        }

    }
}
