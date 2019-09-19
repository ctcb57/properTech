using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormModel model, EmailAddress information)
        {
            if (ModelState.IsValid)
            {
                MailKitEmailService emailService = new MailKitEmailService(new EmailServerConfiguration());
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<ContactFormModel> { model },
                    ToAddresses = new List<EmailAddress> { information },
                    Content = $"Message From {model.Name} \n" +
                    $"Email: {model.Email} \n" + $"Message: {model.Message}",
                    Subject = "Contact Form"
                };

                emailService.Send(msgToSend);
                return RedirectToAction("Index");
            }
            else
            {
                return Contact();
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Property Neighborhood Information
        public IActionResult PropertyInformation()
        {
            var propertyPortfolio = _context.Property.Include("Address").ToList();
            return View(propertyPortfolio);
        }

        public IActionResult PropertyDetails(int id)
        {
            var propertyToShow = _context.Property.Include("Address").FirstOrDefault(p => p.PropertyId == id);
            return View(propertyToShow);
        }

        //Get all information on Vacancies 
        public IActionResult Vacancies()
        {
            var unitVacancies = _context.Unit.Include("Address").ToList();
            return View(unitVacancies);
        }

        //Get Details of Unit that is open
        public IActionResult UnitDetails(int? id)
        {
            var unitToShow = _context.Unit.Include("Address").FirstOrDefault(p => p.UnitId == id);
            return View(unitToShow);
        }
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
