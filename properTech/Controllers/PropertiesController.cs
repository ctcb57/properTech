using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;
using System.Net;
using Newtonsoft.Json;
using properTech.Utility;

namespace properTech.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Properties
        public IActionResult Index()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var manager = _context.Manager.FirstOrDefault(m => m.ApplicationUserId == currentUserId);
            var properties = _context.Property.Where(p => p.ManagerId == manager.ManagerId).ToList();
            return View(properties);
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Property
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        public string ConvertAddressToGoogleFormat(Address address)
        {
            string googleFormatAddress = address.StreetAddress + "," + address.City + "," + address.State + "," + address.ZipCode + "," + address.Country;
            return googleFormatAddress;
        }

        public GeoCode GeoLocate(string address)
        {
            var key = Keys.GoogleGeoCodeAPIKey;
            var requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={key}";
            var result = new WebClient().DownloadString(requestUrl);
            GeoCode geocode = JsonConvert.DeserializeObject<GeoCode>(result);
            return geocode;
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            Property property = new Property();
            return View(property);
        }

        //POST: Properties/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,PropertyName,Address,ManagerId")] Property @property)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                Manager manager = _context.Manager.Where(m => m.ApplicationUserId == currentUserId).Single();
                property.ManagerId = manager.ManagerId;
                Address address = new Address();
                address = property.Address;
                address.Country = "USA";
                string addressToConvert = ConvertAddressToGoogleFormat(address);
                var geoLocate = GeoLocate(addressToConvert);
                address.Longitude = geoLocate.results[0].geometry.location.lng;
                address.Latitude = geoLocate.results[0].geometry.location.lat;
                _context.Add(address);
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyId,PropertyName,Address,ManagerId")] Property @property)
        {
            if (id != @property.PropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.PropertyId))
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
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Property
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Property.FindAsync(id);
            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.PropertyId == id);
        }
    }
}
