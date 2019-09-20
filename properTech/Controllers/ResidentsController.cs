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
    public class ResidentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Residents
        public IActionResult Index()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var resident = _context.Resident.FirstOrDefault(m => m.ApplicationUserId == currentUserId);
            return View(resident);
        }

        // GET: Residents/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Residents/Create
        public IActionResult Create()
        {
            Resident resident = new Resident();
            return View(resident);
        }

        // POST: Residents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidentId,FirstName,LastName,LeaseStart,LeaseEnd,RenewedLease,PaymentDueDate,LatePayment,Balance,UnitId,ApplicationUserId,Email,PhoneNumber,isAssignedUnit")] Resident resident, string id)
        {
            if (ModelState.IsValid)
            {
                resident.ApplicationUserId = id ;
                var currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
                resident.Email = currentUser.Email;
                resident.PhoneNumber = currentUser.PhoneNumber;
                resident.isAssignedUnit = false;
                _context.Add(resident);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = resident.ResidentId });
            }
            return View(resident);
        }

        // GET: Residents/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Residents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResidentId,FirstName,LastName,LeaseStart,LeaseEnd,RenewedLease,PaymentDueDate,LatePayment,Balance,UnitId")] Resident resident)
        {
            if (id != resident.ResidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidentExists(resident.ResidentId))
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
            return View(resident);
        }

        // GET: Residents/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resident = await _context.Resident.FindAsync(id);
            _context.Resident.Remove(resident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidentExists(int id)
        {
            return _context.Resident.Any(e => e.ResidentId == id);
        }

        public IActionResult Overdue()
        {
            var overdueResidents = _context.Resident.Where(r => r.LatePayment == true).ToList();
            return View(overdueResidents);
        }

        public IActionResult Maintenance()
        {
            return View();
        }

        public IActionResult CompletedRequests()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var resident = _context.Resident.FirstOrDefault(m => m.ApplicationUserId == currentUserId);
            var completedRequests = _context.MaintenanceRequest.Where(m => m.ResidentId == resident.ResidentId && m.MaintenanceStatus == "Complete").ToList();
            return View(completedRequests);
        }

        public IActionResult PendingRequests()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var resident = _context.Resident.FirstOrDefault(m => m.ApplicationUserId == currentUserId);
            var pendingRequests = _context.MaintenanceRequest.Where(m => m.ResidentId == resident.ResidentId && m.MaintenanceStatus == "In Progress").ToList();
            return View(pendingRequests);
        }
    }
}
