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
        // Allow us to iterate over a collection of Employees
        public IEnumerable<Employee> Employees;

        // Allow us to iterate over a collection of Training Programs
        public IEnumerable<TrainingProgram> TrainingPrograms;

    }
}
