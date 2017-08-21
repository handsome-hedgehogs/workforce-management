using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HandsomeHedgehogHoedown.Models;

namespace HandsomeHedgehogHoedown.Models
{
    public class HandsomeHedgehogHoedownContext : DbContext
    {
        public HandsomeHedgehogHoedownContext (DbContextOptions<HandsomeHedgehogHoedownContext> options)
            : base(options)
        {
        }

        public DbSet<HandsomeHedgehogHoedown.Models.Department> Department { get; set; }

        public DbSet<HandsomeHedgehogHoedown.Models.Computer> Computer { get; set; }

        public DbSet<HandsomeHedgehogHoedown.Models.Employee> Employee { get; set; }

        public DbSet<HandsomeHedgehogHoedown.Models.EmployeeComputer> EmployeeComputer { get; set; }

        public DbSet<HandsomeHedgehogHoedown.Models.EmployeeTraining> EmployeeTraining { get; set; }

        public DbSet<HandsomeHedgehogHoedown.Models.TrainingProgram> TrainingProgram { get; set; }
    }
}
