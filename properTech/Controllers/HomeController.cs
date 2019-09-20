using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public MapQuestLocationData ReturnLocations(int id)
        {
            var property = _context.Property.Include("Address").FirstOrDefault(p => p.PropertyId == id);
            var key = Keys.MapQuestAPIKey;
            var lat = property.Address.Latitude;
            var lng = property.Address.Longitude;
            var requestUrl = $"http://www.mapquestapi.com/search/v2/radius?key={key}&maxMatches=10&origin={lat},{lng}&radius=15&units=wmin";
            var result = new WebClient().DownloadString(requestUrl);
            MapQuestLocationData placesData = JsonConvert.DeserializeObject<MapQuestLocationData>(result);
            return placesData;
        }

        public List<PointOfInterest> GetListOfPointsOfInterest(int id)
        {
            List<PointOfInterest> pointsOfInterest = new List<PointOfInterest>();
            MapQuestLocationData mapQuestJson = ReturnLocations(id);
            var result = mapQuestJson.searchResults;
            foreach (var item in result)
            {
                PointOfInterest point = new PointOfInterest();
                point.Address = item.fields.address;
                point.Name = item.name;
                point.PhoneNumber = item.fields.phone;
                point.TypeOfBusiness = item.fields.group_sic_code_name;
                pointsOfInterest.Add(point);
            }
            return pointsOfInterest;
        }

        // GET: Property Neighborhood Information
        public IActionResult PropertyInformation()
        {
            var propertyPortfolio = _context.Property.Include("Address").ToList();
            return View(propertyPortfolio);
        }

        public IActionResult PropertyDetails(int id)
        {
            var property = _context.Property.Include(p => p.Address).FirstOrDefault(p => p.PropertyId == id);
            property.PointsOfInterest = GetListOfPointsOfInterest(id);
            return View(property);
        }

        public IActionResult LocalPointsOfInterest(int id)
        {
            var property = _context.Property.FirstOrDefault(p => p.PropertyId == id);
            property.PointsOfInterest = GetListOfPointsOfInterest(id);
            return View(property);
        }

        //Get all information on Vacancies 
        public IActionResult Vacancies()
        {
            var unitVacancies = _context.Unit.Where(u=> u.IsOccupied == false).Include("Address").ToList();
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
