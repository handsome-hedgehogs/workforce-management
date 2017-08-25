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
        // Instance of an Employee
        public Employee Employee { get; set; }

        // Instance of a Department
        public Department Department { get; set; }

        public List<Department> DepartmentList { get; set; }

        // List of all Computers
        public List<Computer> Computer { get; set; }

        // List of all Training Programs
        public List<TrainingProgram> TrainingPrograms { get; set; }

        public List<TrainingProgram> OtherPrograms { get; set; }

        public List<Computer> OtherComputers { get; set; }

        // Constructor method to create object instance of a list of Computers and Training Programs
        public EmployeeDetailViewModel()
        {
            DepartmentList = new List<Department>();
            Computer = new List<Computer>();
            OtherComputers = new List<Computer>();
            OtherPrograms = new List<TrainingProgram>();
            TrainingPrograms = new List<TrainingProgram>();
        }
    }
}
