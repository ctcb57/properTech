using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Get all information on Vacancies 
        public IActionResult Vacancies()
        {


            return View(_context.Unit.Where(u=> u.IsOccupied == false).ToList());
        }

        //Get Details of Unit that is open
        public IActionResult UnitDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = _context.Unit.Find(id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
