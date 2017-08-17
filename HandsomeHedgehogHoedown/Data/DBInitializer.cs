using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HandsomeHedgehogHoedown.Models;
using System.Threading.Tasks;
namespace HandsomeHedgehogHoedown.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HandsomeHedgehogHoedownContext(serviceProvider.GetRequiredService<DbContextOptions<HandsomeHedgehogHoedownContext>>()))
            {
                if (context.Employee.Any())
                {
                    return;
                }
                var employees = new Employee[]
                {
                    new Employee {
                        FirstName = "Gucci",
                        LastName = "Mane"
                    },
                    new Employee{
                        FirstName = "Riff",
                        LastName= "Raff"
                    },
                    new Employee{
                        FirstName = "Waka Flocka",
                        LastName = "Flame"
                    }
                };
                foreach (Employee i in employees)
                {
                    context.Employee.Add(i);
                }
                context.SaveChanges();
                var departments = new Department[]
                {
                    new Department{
                        Name = "Human Resources"
                    },
                    new Department{
                        Name = "Management Overhead"
                    },
                    new Department{
                        Name = "Information Technology"
                    }
                };
                foreach (Department i in departments)
                {
                    context.Department.Add(i);
                }
                context.SaveChanges();

                var computers = new Computer[]
                {
                    new Computer{
                        Manufacturer = "Delli",
                        Make = "XPS13",
                        PurchaseDate = new DateTime(2016, 1, 3)
                    },
                    new Computer{
                        Manufacturer = "Mac",
                        Make = "Needs windows",
                        PurchaseDate = new DateTime(1, 1, 1)
                    },
                    new Computer{
                        Manufacturer = "Da Best",
                        Make = "poopmachine",
                        PurchaseDate = new DateTime(2005, 9, 19)
                    }
                };
                foreach (Computer i in computers)
                {
                    context.Computer.Add(i);
                }
                context.SaveChanges();

                var trainingPrograms = new TrainingProgram[]
                {
                    new TrainingProgram{
                        Name = "Jabronism",
                        StartDate = new DateTime(2001, 6, 12),
                        EndDate = new DateTime(2009, 5, 26),
                        MaxCapacity = 1000000
                    },
                    new TrainingProgram{
                        Name = "How to pronounce gif",
                        StartDate = new DateTime(2018, 2, 5),
                        EndDate = new DateTime(2019, 2, 26),
                        MaxCapacity = 1
                    },
                    new TrainingProgram{
                        Name = "How to have thick rim glasses",
                        StartDate = new DateTime(2017, 8, 19),
                        EndDate = new DateTime(2017, 8, 21),
                        MaxCapacity = 3
                    }
                };
                foreach (TrainingProgram i in trainingPrograms)
                {
                    context.TrainingProgram.Add(i);
                }
                context.SaveChanges();
                var employeeComputers = new EmployeeComputer[]
                {
                    new EmployeeComputer{
                        ComputerId = computers.Single(s => s.PurchaseDate == new DateTime(2017, 08, 01)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "Gucci").EmployeeId
                    },
                        new EmployeeComputer{
                        ComputerId = computers.Single(s => s.PurchaseDate == new DateTime(2017, 08, 03)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "Waka Flocka").EmployeeId
                    },
                        new EmployeeComputer{
                        ComputerId = computers.Single(s => s.PurchaseDate == new DateTime(2017, 08, 06)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "Riff").EmployeeId
                    }
                };
                foreach (EmployeeComputer i in employeeComputers)
                {
                    context.EmployeeComputer.Add(i);
                }
                context.SaveChanges();
                var employeeTrainings = new EmployeeTraining[]
                {
                    new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "Waka Flocka").EmployeeId,
                        TrainingProgramId = trainingPrograms.Single(s => s.Name ==  "Jabronism").TrainingProgramId
                    },
                        new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "Riff").EmployeeId,
                        TrainingProgramId = trainingPrograms.Single(s => s.Name ==  "How to have thick rim glasses").TrainingProgramId
                    },
                        new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "Gucci").EmployeeId,
                        TrainingProgramId = trainingPrograms.Single(s => s.Name == "How to pronounce gif").TrainingProgramId
                    }
                };
                foreach (EmployeeTraining i in employeeTrainings)
                {
                    context.EmployeeTraining.Add(i);
                }
                context.SaveChanges();
            }
        }
    }
}
