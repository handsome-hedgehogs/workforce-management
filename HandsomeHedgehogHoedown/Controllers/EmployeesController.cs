using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandsomeHedgehogHoedown.Models;
using HandsomeHedgehogHoedown.ViewModels;

namespace HandsomeHedgehogHoedown.Controllers
{
    //Employees controller, manages Employee view interactions with Database
    public class EmployeesController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;
        // Establishes connection with the Database
        public EmployeesController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: Employees and employees' department data
        public async Task<IActionResult> Index()
        {
            var handsomeHedgehogHoedownContext = _context.Employee.Include(e => e.Department);
            return View(await handsomeHedgehogHoedownContext.ToListAsync());
        }

        // GET: Employees/Details/5
        // Authored By: Jackie Knight
        // Using include to connect join table of EmployeeComputers and EmployeeTrainings

        public async Task<IActionResult> Details(int? id)
        {
            EmployeeDetailViewModel empDetail = new EmployeeDetailViewModel();

            if (id == null)
            {
                return NotFound();
            }

            empDetail.Employee = await _context.Employee
                .Include(e => e.EmployeeComputers)
                .Include(t => t.EmployeeTrainings)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            foreach(var item in empDetail.Employee.EmployeeComputers)
            {
                Computer computer = await _context.Computer
                .SingleOrDefaultAsync(c => c.ComputerId == item.ComputerId);
                empDetail.Computer.Add(computer);  
            }
            foreach(var item in empDetail.Employee.EmployeeTrainings)
            {
                TrainingProgram trainingProgram = await _context.TrainingProgram
                .SingleOrDefaultAsync(tp => tp.TrainingProgramId == item.TrainingProgramId);
                empDetail.TrainingPrograms.Add(trainingProgram);
            }

            empDetail.Department = await _context.Department
                .SingleOrDefaultAsync(d => d.DepartmentId == empDetail.Employee.DepartmentId);


            if (empDetail == null)
            {
                return NotFound();
            }

            return View(empDetail);
        }

        // GET: Employees/Create
        //Populates department dropdown selector with Department
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
        //Creates new Employee using bound user input
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,DepartmentId,DateStart")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        // Displays edit view that allows a user to select a new computer, training program, department or last name. Accepts the user ID as an argument, and only displays information pertinent to that user.
        // Written by: Eliza Meeks, Jason Smith, Willie Pruitt
        public async Task<IActionResult> Edit(int? id)
        {
            EditEmployeeViewModel empDetail = new EditEmployeeViewModel();

            if (id == null)
            {
                return NotFound();
            }

            empDetail.Employee = await _context.Employee
                .Include(e => e.EmployeeComputers)
                .Include(t => t.EmployeeTrainings)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            empDetail.DepartmentList = await _context.Department.ToListAsync();
            foreach (TrainingProgram t in _context.TrainingProgram)
            {
                if (empDetail.Employee.EmployeeTrainings.Any(et => et.TrainingProgramId == t.TrainingProgramId))
                {
                    empDetail.TrainingPrograms.Add(t);
                }
                else if (t.StartDate >= DateTime.Today)
                {
                    empDetail.OtherPrograms.Add(t);
                }
            }
            foreach (Computer c in _context.Computer)
            {
                if (c.DecommissionedDate == null || c.DecommissionedDate > DateTime.Today)
                {
                    if (empDetail.Employee.EmployeeComputers.Any(ec => ec.ComputerId == c.ComputerId && ec.EndDate == null))
                    {
                        empDetail.Computer.Add(c);
                    }
                    else if (!_context.EmployeeComputer.Any(ec => ec.ComputerId == c.ComputerId) || !_context.EmployeeComputer.Any(ec => ec.ComputerId == c.ComputerId && ec.EndDate != null || ec.EndDate >= DateTime.Today))
                    {
                        empDetail.OtherComputers.Add(c);
                    }
                }
            }

            if (empDetail == null)
            {
                return NotFound();
            }

            return View(empDetail);
        }

        // POST: Employees/Edit/5
        // Allows user to save additions to computers and training programs, and changes to last name and department.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel empDetail)
        {
            if (id != empDetail.Employee.EmployeeId)
            {
                return NotFound();
            }
            // if and if/else loops check to see what changes have been made, and only sends appropriate changes to the database.

            // If both a computer and a training program are being added, add everything to the database.
            if (empDetail.ComputerId != null && empDetail.TrainingId != null)
            {
                //Creates new instances of EmployeeComputer and EmployeeTraining to send up to the database.
                EmployeeComputer employeeComputer = new EmployeeComputer() { EmployeeId = empDetail.Employee.EmployeeId, ComputerId = empDetail.ComputerId ?? default(int) };
                EmployeeTraining employeeTraining = new EmployeeTraining() { EmployeeId = empDetail.Employee.EmployeeId, TrainingProgramId = empDetail.TrainingId ?? default(int) };

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Making changes to the context
                        _context.Update(empDetail.Employee);
                        _context.Add(employeeComputer);
                        _context.Add(employeeTraining);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(empDetail.Employee.EmployeeId))
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
            // If only one training table is being created, only update the training program join table in the database
            } else if (empDetail.ComputerId == null && empDetail.TrainingId != null)
            {
                // Creates a new instance of EmployeeTraining to add to the database.
                EmployeeTraining employeeTraining = new EmployeeTraining() { EmployeeId = empDetail.Employee.EmployeeId, TrainingProgramId = empDetail.TrainingId ?? default(int) };

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Adds and saves changes to the context/database
                        _context.Update(empDetail.Employee);
                        _context.Add(employeeTraining);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(empDetail.Employee.EmployeeId))
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
             // If there's only a computer being added
            } else if (empDetail.ComputerId != null && empDetail.TrainingId == null)
            {
                // Creates a new instance of Employee Computer to send to the database.
                EmployeeComputer employeeComputer = new EmployeeComputer() { EmployeeId = empDetail.Employee.EmployeeId, ComputerId = empDetail.ComputerId ?? default(int) };

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Updates and saves changes to the context
                        _context.Update(empDetail.Employee);
                        _context.Add(employeeComputer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(empDetail.Employee.EmployeeId))
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
            }
            // If no computer or training program are being added.
            else if (empDetail.ComputerId == null && empDetail.TrainingId == null)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        // Updates changes to the database.
                        _context.Update(empDetail.Employee);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(empDetail.Employee.EmployeeId))
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
            }
            
            return RedirectToAction("Index");
        }
        

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeId == id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }


        // Update method called from Edit Employee View delete button for computers assigned to an employee
        // accepts ComputerId and EmplyeeId as arguments
        // will return view of DeleteEC html view to confirm delete
        // query checks joined table of employeecomputer to see if there is a computer with a corresponding employeeID
        // if true select that row in joined table
        // Authored by : Tamela Lerma
        public async Task<IActionResult> DeleteEC(int id, int empId)
        {
            var em = await _context.EmployeeComputer
                        .Include(e => e.Computer)
                        .Include(e => e.Employee)
                        .SingleOrDefaultAsync(m => m.ComputerId == id && m.EmployeeId == empId);
            return View(em);
        }

        // Once page is directed to confirm delete
        // Database is called to update EndDate to current DateTime on Employeecomputer table
        // Once end date is added, this computer will no longer be displayed as an assigned computer for that employee
        // DeleteEC template calls this method once user clicks yes to confirm delete
        // ComputerId and EmployeeId are passed in to reference the JoinedTable
        // Authored by : Tamela Lerma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedEC(int ComputerId, int EmployeeId)
        {
            var employeeComputer = await _context.EmployeeComputer.SingleOrDefaultAsync(m => m.ComputerId == ComputerId && m.EmployeeId == EmployeeId);
            employeeComputer.EndDate = DateTime.Today;
            _context.EmployeeComputer.Update(employeeComputer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete method called from Edit Employee View delete button for Training Programs assigned to an employee
        // accepts TrainingProgramId and EmplyeeId as arguments
        // will return view of DeleteTP html view to confirm delete
        // query checks joined table of EmployeeTraining to see if there is a program with a corresponding employeeID
        // if true select that row in joined table
        // Authored by : Eliza Meeks
        public async Task<IActionResult> DeleteTP(int id, int empId)
        {
            var em = await _context.EmployeeTraining
                        .Include(e => e.TrainingProgram)
                        .Include(e => e.Employee)
                        .SingleOrDefaultAsync(m => m.TrainingProgramId == id && m.EmployeeId == empId);
            return View(em);
        }

        // Once page is directed to confirm delete
        // Database is called to Delete row  on EmployeeTraining table
        // Once deleted, this program will no longer have this employee assigned to it
        // DeleteTP template calls DeleteConfirmedTP method once user clicks yes to confirm delete
        // TrainingProgramId and EmployeeId are passed in to reference the JoinedTable
        // Authored by : Eliza Meeks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTP(int TrainingProgramId, int EmployeeId)
        {
            var employeeTraining = await _context.EmployeeTraining.SingleOrDefaultAsync(m => m.TrainingProgramId == TrainingProgramId && m.EmployeeId == EmployeeId);
            _context.EmployeeTraining.Remove(employeeTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

