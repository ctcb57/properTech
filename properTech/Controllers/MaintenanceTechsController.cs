using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class MaintenanceTechsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceTechsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceTechs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaintenanceRequest.Where(r => r.MaintenanceStatus == "Pending").ToListAsync());
        }

        // GET: MaintenanceTechs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.MaintenanceTech
                .FirstOrDefaultAsync(m => m.MaintenanceTechId == id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }

            return View(maintenanceTech);
        }

        // GET: MaintenanceTechs/Create
        public IActionResult Create()
        {
            MaintenanceTech maintenanceTech = new MaintenanceTech();
            return View(maintenanceTech);
        }

        // POST: MaintenanceTechs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintenanceTechId,FirstName,LastName,ApplicationUserId,AverageDeviation")] MaintenanceTech maintenanceTech, string id)
        {
            if (ModelState.IsValid)
            {
                maintenanceTech.ApplicationUserId = id;
                maintenanceTech.TotalRequestCompletions = 0;
                maintenanceTech.TotalTimeSpan = new TimeSpan(0, 0, 0, 0);
                var currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
                _context.Add(maintenanceTech);
                await _context.SaveChangesAsync();
                return RedirectToAction("RequestsView", new { id = maintenanceTech.MaintenanceTechId });
            }
            return View(maintenanceTech);
        }

        // GET: MaintenanceTechs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.MaintenanceTech.FindAsync(id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }
            return View(maintenanceTech);
        }

        // POST: MaintenanceTechs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintenanceTechId,FirstName,LastName,ApplicationUserId")] MaintenanceTech maintenanceTech)
        {
            if (id != maintenanceTech.MaintenanceTechId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceTech);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceTechExists(maintenanceTech.MaintenanceTechId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceTech);
        }

        // GET: MaintenanceTechs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.MaintenanceTech
                .FirstOrDefaultAsync(m => m.MaintenanceTechId == id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }

            return View(maintenanceTech);
        }

        // POST: MaintenanceTechs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceTech = await _context.MaintenanceTech.FindAsync(id);
            _context.MaintenanceTech.Remove(maintenanceTech);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceTechExists(int id)
        {
            return _context.MaintenanceTech.Any(e => e.MaintenanceTechId == id);
        }

        public IActionResult RequestsView()
        {
            var context = _context.MaintenanceRequest.ToList();
            return View(context);
        }
    }
}
