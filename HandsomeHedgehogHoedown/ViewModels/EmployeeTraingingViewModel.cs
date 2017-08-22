using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandsomeHedgehogHoedown.Models;

namespace HandsomeHedgehogHoedown.ViewModels
{
    public class EmployeeTrainingViewModel
    {
        public TrainingProgram tp { get; set; }
        public List<Employee> Employees {get; set;}

        public EmployeeTrainingViewModel()
        {
            Employees = new List<Employee>();
        }

    }
}
