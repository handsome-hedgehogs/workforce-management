using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandsomeHedgehogHoedown.Models;

namespace HandsomeHedgehogHoedown.Controllers
{
    //Computers Controller, manages Computer view interactions with Database
    public class ComputersController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;
        //Establishes connection with the Database
        public ComputersController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: Computers
        //Returns view of a List of Computers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Computer.ToListAsync());
        }

        // GET: Computers/Details/5
        //Returns view of Computer with Id of Id passed in url
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .SingleOrDefaultAsync(m => m.ComputerId == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        //Creates Computer based on bound user input from view
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComputerId,Manufacturer,Make,PurchaseDate,DecommissionedDate")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(computer);
        }

        // GET: Computers/Edit/5
        //Returns view of editable computer information based on Id in url
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.SingleOrDefaultAsync(m => m.ComputerId == id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        //Edits data of Computer based on Id in url, with user input bound from view
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComputerId,Manufacturer,Make,PurchaseDate,DecommissionedDate")] Computer computer)
        {
            if (id != computer.ComputerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(computer);
                if (computer.DecommissionedDate != null)
                {
                    EmployeeComputer ec = await _context.EmployeeComputer.LastOrDefaultAsync(c => c.ComputerId == computer.ComputerId);
                    if (ec != null)
                    {
                        ec.EndDate = computer.DecommissionedDate;
                        _context.Update(ec);
                    }
                }
                await _context.SaveChangesAsync();

                try
                {
                    _context.Update(computer);
                    if(computer.DecommissionedDate != null)
                    {
                        EmployeeComputer ec = await _context.EmployeeComputer.LastOrDefaultAsync(c => c.ComputerId == computer.ComputerId);
                        ec.EndDate = computer.DecommissionedDate;
                        _context.Update(ec);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.ComputerId))
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
            return View(computer);
        }

        // GET: Computers/Delete/5
        //Returns view of computer details of computer based on Id in url
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .SingleOrDefaultAsync(m => m.ComputerId == id);
            if (computer == null)
            {
                return NotFound();
            }

            if (_context.EmployeeComputer.Any(c => c.ComputerId == id)) {
                return View();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        //Deletes Computer from DB based on Id in url
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computer.SingleOrDefaultAsync(m => m.ComputerId == id);
            _context.Computer.Remove(computer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Returns true if any computer with Id in url exists
        private bool ComputerExists(int id)
        {
            return _context.Computer.Any(e => e.ComputerId == id);
        }
    }
}
