using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandsomeHedgehogHoedown.Models;
using HandsomeHedgehogHoedown.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HandsomeHedgehogHoedown.Controllers
{
    public class TrainingProgramsController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;

        public TrainingProgramsController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: TrainingPrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingProgram.ToListAsync());
        }

        // GET: TrainingPrograms/Details/5
        // insert viewModel EmplyoyeeTrainingViewModel object instance
        // ViewModel includes object instance of List<Employee> to add Employee Object that is assigned to a Training Program
        // Authored by : Tamela Lerma
        public async Task<IActionResult> Details(int? id)
        {
            EmployeeTrainingViewModel employeesTP = new EmployeeTrainingViewModel();

            if (id == null)
            {
                return NotFound();
            }

            // use view model instance, call instance of training program model within ViewModel
            // call TP Model, Include Icollection join table EmployeeTrainings
            // get Single object that has TrainingProgramId that is equal to id that is passed as argument
            // this will list details for a single training program  that is selected
            // Authored by : Tamela Lerma
            employeesTP.tp = await _context.TrainingProgram.Include(et => et.EmployeeTrainings)
                .SingleOrDefaultAsync(m => m.TrainingProgramId == id);
            // iterate through ICollection of EmployeeTrainings
            foreach (var item in employeesTP.tp.EmployeeTrainings) {
                // create a var of type employee and query Employee table to find EmployeeID that matches an EmployeeID from joined table in ICollection
                Employee employee = await _context.Employee.SingleOrDefaultAsync(e => e.EmployeeId == item.EmployeeId);
                // Once employee object is selected, add to List<Employee> Employees on the ViewModel
                employeesTP.Employees.Add(employee);
            }

            if (employeesTP.tp == null)
            {
                return NotFound();
            }
            // pass ViewModel instance that Includes Joined Table Collection
            return View(employeesTP);
        }

        // GET: TrainingPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingProgramId,Name,StartDate,EndDate,MaxCapacity")] TrainingProgram trainingProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Edit/5
        //Return view of Employ
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingProgram.SingleOrDefaultAsync(m => m.TrainingProgramId == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }
            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Edit/5
        //Edits data of TrainingProgram based on Id in url, with user input bound from view
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingProgramId,Name,StartDate,EndDate,MaxCapacity")] TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.TrainingProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingProgramExists(trainingProgram.TrainingProgramId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(trainingProgram);
        }
        //Returns true if TrainingProgam in url exists
        private bool TrainingProgramExists(int id)
        {
            return _context.TrainingProgram.Any(e => e.TrainingProgramId == id);
        }
    }
}
