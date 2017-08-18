using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HandsomeHedgehogHoedown.Models;
using HandsomeHedgehogHoedown.ViewModels;

namespace HandsomeHedgehogHoedown.Controllers
{
    public class HomeController : Controller
    {

        private readonly HandsomeHedgehogHoedownContext _context;

        public HomeController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Employees = _context.Employee.OrderBy(o => o.DateStart).Take(5).ToList();
            model.TrainingPrograms = _context.TrainingProgram.Where(o => DateTime.Now <= o.StartDate && DateTime.Now.AddDays(28) >= o.StartDate).ToList();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
