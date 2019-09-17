using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class MaintenanceTechesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceTechesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceTeches
        public async Task<IActionResult> Index()
        {
            return View(await _context.maintenanceTeches.ToListAsync());
        }

        // GET: MaintenanceTeches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.maintenanceTeches
                .FirstOrDefaultAsync(m => m.MaintenanceTechId == id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }

            return View(maintenanceTech);
        }

        // GET: MaintenanceTeches/Create
        public IActionResult Create(string id)
        {
            return View();
        }

        // POST: MaintenanceTeches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintenanceTechId,FirstName,LastName,ApplicationUserId")] MaintenanceTech maintenanceTech, string id)
        {
            if (ModelState.IsValid)
            {
                maintenanceTech.ApplicationUserId = id;
                _context.Add(maintenanceTech);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceTech);
        }

        // GET: MaintenanceTeches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.maintenanceTeches.FindAsync(id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }
            return View(maintenanceTech);
        }

        // POST: MaintenanceTeches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintenanceTechId,FirstName,LastName")] MaintenanceTech maintenanceTech)
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

        // GET: MaintenanceTeches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.maintenanceTeches
                .FirstOrDefaultAsync(m => m.MaintenanceTechId == id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }

            return View(maintenanceTech);
        }

        // POST: MaintenanceTeches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceTech = await _context.maintenanceTeches.FindAsync(id);
            _context.maintenanceTeches.Remove(maintenanceTech);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceTechExists(int id)
        {
            return _context.maintenanceTeches.Any(e => e.MaintenanceTechId == id);
        }
    }
}
