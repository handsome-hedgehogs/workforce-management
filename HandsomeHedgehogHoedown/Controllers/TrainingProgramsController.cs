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
        public async Task<IActionResult> Details(int? id)
        {
            EmployeeTrainingViewModel employeesTP = new EmployeeTrainingViewModel();

            if (id == null)
            {
                return NotFound();
            }

            employeesTP.tp = await _context.TrainingProgram.Include(et => et.EmployeeTrainings)
                .SingleOrDefaultAsync(m => m.TrainingProgramId == id);

            foreach (var item in employeesTP.tp.EmployeeTrainings) {
                Employee employee = await _context.Employee.SingleOrDefaultAsync(e => e.EmployeeId == item.EmployeeId);
                employeesTP.Employees.Add(employee);
            }


            if (employeesTP.tp == null)
            {
                return NotFound();
            }

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

        // GET: TrainingPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingProgram
                .SingleOrDefaultAsync(m => m.TrainingProgramId == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingProgram = await _context.TrainingProgram.SingleOrDefaultAsync(m => m.TrainingProgramId == id);
            _context.TrainingProgram.Remove(trainingProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TrainingProgramExists(int id)
        {
            return _context.TrainingProgram.Any(e => e.TrainingProgramId == id);
        }
    }
}
