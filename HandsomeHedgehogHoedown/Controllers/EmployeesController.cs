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
    public class EmployeesController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;

        public EmployeesController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: Employees
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
            
            foreach (var item in empDetail.Employee.EmployeeComputers)
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
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
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
        public async Task<IActionResult> Edit(int? id)
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
            empDetail.DepartmentList = await _context.Department.ToListAsync();
            foreach(TrainingProgram t in _context.TrainingProgram)
            {
                if(empDetail.Employee.EmployeeTrainings.Any(et => et.TrainingProgramId == t.TrainingProgramId))
                {
                    empDetail.TrainingPrograms.Add(t);
                } else
                {
                    empDetail.OtherPrograms.Add(t);
                }
            }
            foreach (Computer c in _context.Computer)
            {
                if (empDetail.Employee.EmployeeComputers.Any(ec => ec.ComputerId == c.ComputerId))
                {
                    empDetail.Computer.Add(c);
                }
                else if(!_context.EmployeeComputer.Any(ec => ec.ComputerId == c.ComputerId) || !_context.EmployeeComputer.Any(ec => ec.EndDate != null && ec.ComputerId == c.ComputerId))
                {
                    empDetail.OtherComputers.Add(c);
                }
            }

            if (empDetail == null)
            {
                return NotFound();
            }

            return View(empDetail);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,DepartmentId,DateStart")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }
        //Value currently null, needs to be current employees current dep.
        //private void PopulateDapartmentsDropDownList(object selectedDepartment = null)
        //{
        //    var departmentsQuery = from d in _context.Department
        //                           orderby d.Name
        //                           select d;
        //    ViewBag.DepartmentId = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentId", "Name", selectedDepartment);
        //}

        //private void PopulateComputerDropDownList(object selectedComputer = null)
        //{
        //    var computerQuery = from c in _context.Computer
        //                        //join ec in _context.EmployeeComputer
        //                        //on c.ComputerId
        //                        //equals ec.ComputerId
        //                        select c;
        //    ViewBag.ComputerMake = new SelectList(computerQuery.AsNoTracking(), "ComputerId", "Make",
        //        selectedComputer);
        //}

        //private void PopulateTrainingDropDownList()
        //{
        //    var trainingProgramQuery = from tp in _context.TrainingProgram
        //                               where tp.StartDate > DateTime.Now
        //                               select tp.Name;
        //    ViewBag.TrainingProgram = new SelectList(trainingProgramQuery.AsNoTracking(), "Name");
        //}

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
    }
}
