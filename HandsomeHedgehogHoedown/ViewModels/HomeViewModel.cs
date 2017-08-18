using HandsomeHedgehogHoedown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Employee> Employees;
        public IEnumerable<TrainingProgram> TrainingPrograms;
    }
}
