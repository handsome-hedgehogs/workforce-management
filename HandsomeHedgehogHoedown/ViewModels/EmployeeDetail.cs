using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandsomeHedgehogHoedown.Models;

// Authored By: Jackie Knight
// Created this View Model for Employee Detail
// Including all of these instances and lists to join all the information needed when viewing a specific employee's details

namespace HandsomeHedgehogHoedown.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public Employee Employee { get; set; }

        public Department Department { get; set; }

        public List<Computer> Computer { get; set; }

        public List<TrainingProgram> TrainingPrograms { get; set; }

        public EmployeeDetailViewModel()
        {
            Computer = new List<Computer>();
            TrainingPrograms = new List<TrainingProgram>();
        }
    }
}
