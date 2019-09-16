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
            var applicationDbContext = _context.maintenanceTeches.Include(m => m.resident);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MaintenanceTeches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceTech = await _context.maintenanceTeches
                .Include(m => m.resident)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceTech == null)
            {
                return NotFound();
            }

            return View(maintenanceTech);
        }

        // GET: MaintenanceTeches/Create
        public IActionResult Create()
        {
            ViewData["residentId"] = new SelectList(_context.Resident, "Id", "Id");
            return View();
        }

        // POST: MaintenanceTeches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,firstName,lastName,residentId")] MaintenanceTech maintenanceTech)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceTech);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["residentId"] = new SelectList(_context.Resident, "Id", "Id", maintenanceTech.residentId);
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
            ViewData["residentId"] = new SelectList(_context.Resident, "Id", "Id", maintenanceTech.residentId);
            return View(maintenanceTech);
        }

        // POST: MaintenanceTeches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,firstName,lastName,residentId")] MaintenanceTech maintenanceTech)
        {
            if (id != maintenanceTech.Id)
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
                    if (!MaintenanceTechExists(maintenanceTech.Id))
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
            ViewData["residentId"] = new SelectList(_context.Resident, "Id", "Id", maintenanceTech.residentId);
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
                .Include(m => m.resident)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return _context.maintenanceTeches.Any(e => e.Id == id);
        }
    }
}
