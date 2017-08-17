using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandsomeHedgehogHoedown.Models;

namespace HandsomeHedgehogHoedown.Controllers
{
    public class EmployeeTrainingsController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;

        public EmployeeTrainingsController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: EmployeeTrainings
        public async Task<IActionResult> Index()
        {
            var handsomeHedgehogHoedownContext = _context.EmployeeTraining.Include(e => e.Employee).Include(e => e.TrainingProgram);
            return View(await handsomeHedgehogHoedownContext.ToListAsync());
        }

        // GET: EmployeeTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTraining = await _context.EmployeeTraining
                .Include(e => e.Employee)
                .Include(e => e.TrainingProgram)
                .SingleOrDefaultAsync(m => m.EmployeeTrainingId == id);
            if (employeeTraining == null)
            {
                return NotFound();
            }

            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            ViewData["TrainingProgramId"] = new SelectList(_context.Set<TrainingProgram>(), "TrainingProgramId", "Name");
            return View();
        }

        // POST: EmployeeTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeTrainingId,EmployeeId,TrainingProgramId")] EmployeeTraining employeeTraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeTraining.EmployeeId);
            ViewData["TrainingProgramId"] = new SelectList(_context.Set<TrainingProgram>(), "TrainingProgramId", "Name", employeeTraining.TrainingProgramId);
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTraining = await _context.EmployeeTraining.SingleOrDefaultAsync(m => m.EmployeeTrainingId == id);
            if (employeeTraining == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeTraining.EmployeeId);
            ViewData["TrainingProgramId"] = new SelectList(_context.Set<TrainingProgram>(), "TrainingProgramId", "Name", employeeTraining.TrainingProgramId);
            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeTrainingId,EmployeeId,TrainingProgramId")] EmployeeTraining employeeTraining)
        {
            if (id != employeeTraining.EmployeeTrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeTraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTrainingExists(employeeTraining.EmployeeTrainingId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeTraining.EmployeeId);
            ViewData["TrainingProgramId"] = new SelectList(_context.Set<TrainingProgram>(), "TrainingProgramId", "Name", employeeTraining.TrainingProgramId);
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTraining = await _context.EmployeeTraining
                .Include(e => e.Employee)
                .Include(e => e.TrainingProgram)
                .SingleOrDefaultAsync(m => m.EmployeeTrainingId == id);
            if (employeeTraining == null)
            {
                return NotFound();
            }

            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeTraining = await _context.EmployeeTraining.SingleOrDefaultAsync(m => m.EmployeeTrainingId == id);
            _context.EmployeeTraining.Remove(employeeTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmployeeTrainingExists(int id)
        {
            return _context.EmployeeTraining.Any(e => e.EmployeeTrainingId == id);
        }
    }
}
