﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;


namespace properTech.Controllers
{
    public class ManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private readonly UserManager<IdentityUser> _userManager;

        public ManagersController(ApplicationDbContext context)
        {
            _context = context;
        }
        //viewallresidents
        //getoverdueaccounts
        //modify resident
        //

        public IActionResult GetLateResidents()
        {
            var yesterday = DateTime.Now.AddDays(-1).DayOfYear.ToString();
            var paymentMissed = _context.Resident.Where(s => s.PaymentDueDate.ToString() == yesterday).ToList();
            if (paymentMissed != null)
            {
                foreach (Resident resident in paymentMissed)
                {
                    resident.LatePayment = true;
                }
            }
            var lateResidents = _context.Resident.Where(r => r.LatePayment == true).ToList();
            return View(lateResidents);
        }


        // GET: Managers
        public IActionResult Index()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var manager = _context.Manager.FirstOrDefault(m => m.ApplicationUserId == currentUserId);
            return View(manager);
        }

        // GET: Edit Residents
        public IActionResult GetResidents()
        {
            List<Resident> residentList = new List<Resident>();
            var residents = _context.Resident;
            foreach(var resident in residents)
            {
                residentList.Add(resident);
            }
            return View(residentList);
        }
        // GET: Edit Single Resident
        public async Task<IActionResult> EditResident(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await _context.Resident.FindAsync(id);
            if (resident == null)
            {
                return NotFound();
            }
            return View(resident);
        }
        // POST: Manager/EditResident
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResident(int id, [Bind("ResidentId,FirstName,LastName,LeaseStart,LeaseEnd,RenewedLease,PaymentDueDate,LatePayment,Balance,UnitId,ApplicationUserId,isAssignedUnit,UnitNumber")] Resident resident)
        {
            if (id != resident.ResidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(resident).State = EntityState.Modified;
                _context.SaveChanges();
                var unitToMatch = MatchUnit(resident);
                unitToMatch.IsOccupied = true;
                resident.UnitId = unitToMatch.UnitId;
                _context.Entry(resident).State = EntityState.Modified;
                _context.Entry(unitToMatch).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("GetResidents");
            }
            return View(resident);
        }

        // GET: Managers/DeleteResidents
        public async Task<IActionResult> DeleteResident(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resident = await _context.Resident
                .FirstOrDefaultAsync(m => m.ResidentId == id);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        // POST: Managers/DeleteResdient/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResidentDeleteConfirmed(int id)
        {
            var resident = await _context.Resident.FindAsync(id);
            _context.Resident.Remove(resident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public Unit MatchUnit(Resident resident)
        {
            var unitToMatch = _context.Unit.FirstOrDefault(u => u.UnitNumber == resident.UnitNumber);
            return unitToMatch;
        }


        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create

        public IActionResult Create()
        {
            Manager manager = new Manager();
            return View(manager);
        }

        // POST: Managers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,FirstName,LastName,ApplicationUserId")] Manager manager, string id)
        {
            if (ModelState.IsValid)
            {
                manager.ApplicationUserId = id;
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,FirstName,LastName")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerId))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Manager.FindAsync(id);
            _context.Manager.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Manager.Any(e => e.ManagerId == id);
        }
        public IActionResult Data()
        {
            var data = _context;
            return View(data);
        }
    }
}
