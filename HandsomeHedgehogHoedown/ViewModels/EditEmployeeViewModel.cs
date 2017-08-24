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
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Computer> Computers { get; set; }
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }
        public EditEmployeeViewModel (Employee employee)
        {
            Employee = employee;

            Departments = new List<Department>();

            Computers = new List<Computer>();

            TrainingPrograms = new List<TrainingProgram>();

        }
    }
}
