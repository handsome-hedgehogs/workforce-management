using HandsomeHedgehogHoedown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.ViewModels
{
    //View model for the Home page, holds IEnumerables of Employee and Trianing Program
    //Written by Eliza Meeks
    public class HomeViewModel
    {
        public IEnumerable<Employee> Employees;
        public IEnumerable<TrainingProgram> TrainingPrograms;
    }
}
