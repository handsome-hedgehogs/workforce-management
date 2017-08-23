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
    //Home controller, populates the home view model with the correct data.
    //Written by Eliza W Meeks
    public class HomeController : Controller
    {

        private readonly HandsomeHedgehogHoedownContext _context;
        // Establishes connection with the database
        public HomeController(HandsomeHedgehogHoedownContext context)
        {
            _context = context;
        }
        //Populates the view model with the correct data and then returns the data to the view.
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Employees = _context.Employee.OrderByDescending(o => o.DateStart).Take(5).ToList();
            model.TrainingPrograms = _context.TrainingProgram.Where(o => DateTime.Now <= o.StartDate && DateTime.Now.AddDays(28) >= o.StartDate).ToList();
            return View(model);
        }
    }
}
