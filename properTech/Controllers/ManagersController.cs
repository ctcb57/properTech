using System;
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
        // GET: Assign Users
        public IActionResult AssignUsers()
        {
            var unassignedUserRoleObject = _context.Roles.FirstOrDefault(r => r.Name == "UnassignedUser");
            var unassignedUserAppUserIds = _context.UserRoles.Where(u => u.RoleId == unassignedUserRoleObject.Id).ToList();
            foreach(var user in unassignedUserAppUserIds)
            {
                var userObject = _context.Users.FirstOrDefault(u => u.Id == user.UserId);
                UnassignedUsers unassignedUser = new UnassignedUsers();
                unassignedUser.Email = userObject.Email;
                unassignedUser.ApplicationUserId = userObject.Id;
                _context.Add(unassignedUser);
                _context.SaveChanges();
            }
            var unassignedUsers = GetUnassignedUsers();
            return View(unassignedUsers);
        }

        // GET: AssignResident
        public IActionResult AssignResident(int id)
        {
            var residentToAssign = _context.UnassignedUsers.FirstOrDefault(u => u.UnassignedId == id);
            var user = _context.UserRoles.FirstOrDefault(u => u.UserId == residentToAssign.ApplicationUserId);
            var residentRoleObject = _context.Roles.FirstOrDefault(r => r.Name == "Resident");
            _context.UserRoles.Remove(user);
            _context.SaveChanges();
            user.RoleId = residentRoleObject.Id;
            user.UserId = residentToAssign.ApplicationUserId;
            _context.Add(user);
            _context.SaveChanges();
            Resident resident = new Resident();
            resident.ApplicationUserId = residentToAssign.ApplicationUserId;
            _context.Add(resident);
            _context.SaveChanges();
            var unassignedUserToRemove = _context.UnassignedUsers.FirstOrDefault(u => u.ApplicationUserId == resident.ApplicationUserId);
            _context.UnassignedUsers.Remove(unassignedUserToRemove);
            _context.SaveChanges();
            return RedirectToAction("EditResident", "Managers", new { id = resident.ResidentId });
        }

        public IActionResult AssignMaintenance(int id)
        {
            var residentToAssign = _context.UnassignedUsers.FirstOrDefault(u => u.UnassignedId == id);
            var user = _context.UserRoles.FirstOrDefault(u => u.UserId == residentToAssign.ApplicationUserId);
            var residentRoleObject = _context.Roles.FirstOrDefault(r => r.Name == "Maintenance");
            _context.UserRoles.Remove(user);
            _context.SaveChanges();
            user.RoleId = residentRoleObject.Id;
            user.UserId = residentToAssign.ApplicationUserId;
            _context.Add(user);
            _context.SaveChanges();
            MaintenanceTech tech = new MaintenanceTech();
            tech.ApplicationUserId = residentToAssign.ApplicationUserId;
            _context.Add(tech);
            _context.SaveChanges();
            var unassignedUserToRemove = _context.UnassignedUsers.FirstOrDefault(u => u.ApplicationUserId == tech.ApplicationUserId);
            _context.UnassignedUsers.Remove(unassignedUserToRemove);
            _context.SaveChanges();
            return RedirectToAction("EditMaintenance", "Managers", new { id = tech.MaintenanceTechId });
        }

        public List<UnassignedUsers> GetUnassignedUsers()
        {
            var unassignedUsers = _context.UnassignedUsers.ToList();
            return unassignedUsers;
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
        public async Task<IActionResult> EditResident(int id, [Bind("ResidentId,FirstName,LastName,LeaseStart,LeaseEnd,RenewedLease,PaymentDueDate,LatePayment,Balance,UnitId,ApplicationUserId,isAssignedUnit,UnitNumber,Email,PhoneNumber")] Resident resident)
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
        // GET: EditMaintenance
        public async Task<IActionResult> EditMaintenance(int? id)
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
        // POST: EditMaintenance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMaintenance(int id, [Bind("MaintenanceTechId,FirstName,LastName,ApplicationUserId,AverageDeviation")] MaintenanceTech maintenanceTech)
        {
            if (id != maintenanceTech.MaintenanceTechId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(maintenanceTech).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintenanceTech);
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
