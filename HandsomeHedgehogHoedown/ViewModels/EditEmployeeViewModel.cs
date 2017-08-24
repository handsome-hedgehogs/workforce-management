using HandsomeHedgehogHoedown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.ViewModels
{
    public class EditEmployeeViewModel
    {
        public Employee Employee { get; set; }

        public EmployeeComputer EmployeeComputer { get; set; }

        public EmployeeTraining EmployeeTraining { get; set; }

        public List<Department> DepartmentList { get; set; }

        public List<Computer> Computer { get; set; }

        public List<TrainingProgram> TrainingPrograms { get; set; }

        public EditEmployeeViewModel()
        {
            DepartmentList = new List<Department>();
            Computer = new List<Computer>();
            TrainingPrograms = new List<TrainingProgram>();
        }
    }
}
