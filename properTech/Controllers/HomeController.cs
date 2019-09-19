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
