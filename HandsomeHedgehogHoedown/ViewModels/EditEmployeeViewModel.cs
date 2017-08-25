using HandsomeHedgehogHoedown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandsomeHedgehogHoedown.Controllers;

namespace HandsomeHedgehogHoedown.ViewModels
{
    // Written by Eliza Meeks, Tamerla Lerma, Jason Smith
    // View Model for the Edit Employee view
    public class EditEmployeeViewModel
    {
        public Employee Employee { get; set; }

        public int? ComputerId { get; set; }

        public int? TrainingId { get; set; }

        public List<Department> DepartmentList { get; set; }

        public List<Computer> Computer { get; set; }

        public List<TrainingProgram> TrainingPrograms { get; set; }

        public List<TrainingProgram> OtherPrograms { get; set; }

        public List<Computer> OtherComputers { get; set; }

        public EmployeeComputersController EmpCT { get; set; }

        // Creates and instance of the following lists everytime a new instance of this class is created.
        public EditEmployeeViewModel()
        {
            DepartmentList = new List<Department>();
            Computer = new List<Computer>();
            OtherComputers = new List<Computer>();
            OtherPrograms = new List<TrainingProgram>();
            TrainingPrograms = new List<TrainingProgram>();
        }
        
    }
}
