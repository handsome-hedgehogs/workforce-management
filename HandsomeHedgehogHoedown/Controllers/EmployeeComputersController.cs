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
    public class EmployeeComputersController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;

        public EmployeeComputersController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: EmployeeComputers
        public async Task<IActionResult> Index()
        {
            var handsomeHedgehogHoedownContext = _context.EmployeeComputer.Include(e => e.Computer).Include(e => e.Employee);
            return View(await handsomeHedgehogHoedownContext.ToListAsync());
        }

        // GET: EmployeeComputers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeComputer = await _context.EmployeeComputer
                .Include(e => e.Computer)
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeComputerId == id);
            if (employeeComputer == null)
            {
                return NotFound();
            }

            return View(employeeComputer);
        }

        // GET: EmployeeComputers/Create
        public IActionResult Create()
        {
            ViewData["ComputerId"] = new SelectList(_context.Computer, "ComputerId", "Make");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            return View();
        }

        // POST: EmployeeComputers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeComputerId,EmployeeId,ComputerId,StartDate,EndDate")] EmployeeComputer employeeComputer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeComputer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ComputerId"] = new SelectList(_context.Computer, "ComputerId", "Make", employeeComputer.ComputerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeComputer.EmployeeId);
            return View(employeeComputer);
        }

        // GET: EmployeeComputers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeComputer = await _context.EmployeeComputer.SingleOrDefaultAsync(m => m.EmployeeComputerId == id);
            if (employeeComputer == null)
            {
                return NotFound();
            }
            ViewData["ComputerId"] = new SelectList(_context.Computer, "ComputerId", "Make", employeeComputer.ComputerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeComputer.EmployeeId);
            return View(employeeComputer);
        }

        // POST: EmployeeComputers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeComputerId,EmployeeId,ComputerId,StartDate,EndDate")] EmployeeComputer employeeComputer)
        {
            if (id != employeeComputer.EmployeeComputerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeComputer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeComputerExists(employeeComputer.EmployeeComputerId))
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
            ViewData["ComputerId"] = new SelectList(_context.Computer, "ComputerId", "Make", employeeComputer.ComputerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", employeeComputer.EmployeeId);
            return View(employeeComputer);
        }

        // GET: EmployeeComputers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeComputer = await _context.EmployeeComputer
                .Include(e => e.Computer)
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(m => m.EmployeeComputerId == id);
            if (employeeComputer == null)
            {
                return NotFound();
            }

            return View(employeeComputer);
        }

        // POST: EmployeeComputers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeComputer = await _context.EmployeeComputer.SingleOrDefaultAsync(m => m.EmployeeComputerId == id);
            _context.EmployeeComputer.Remove(employeeComputer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmployeeComputerExists(int id)
        {
            return _context.EmployeeComputer.Any(e => e.EmployeeComputerId == id);
        }
    }
}
