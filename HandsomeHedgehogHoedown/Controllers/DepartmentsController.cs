using HandsomeHedgehogHoedown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

// this controller manages all actions done on departments
// authored by : Jason Smith
namespace HandsomeHedgehogHoedown.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly HandsomeHedgehogHoedownContext _context;
        //Establishes connection with the Database
        public DepartmentsController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;    
        }

        // GET: Departments
        //Returns view of a List of Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
        }

        // GET: Departments/Details/5
        //Returns view of Department of Id passed in url
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .SingleOrDefaultAsync(m => m.DepartmentId == id);
            // populate the list of employees with employees in that department
            department.Employees = await _context.Employee.Where(e => e.DepartmentId == id).ToListAsync();
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        //Creates Department base on bound user input from view
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        //Returns true if Department Exists
        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}