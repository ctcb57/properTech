using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class MaintenanceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public MaintenanceRequestsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: MaintenanceRequests
        public async Task<IActionResult> Index()
        {
                var applicationDbContext = _context.MaintenanceRequest.Where(m => m.MaintenanceStatus == "Pending");
                return View(await applicationDbContext.ToListAsync());

        }

        public IActionResult ViewMaintenanceRequests()
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var currentTech = _context.MaintenanceTech.Where(m => m.ApplicationUserId == currentUserId).FirstOrDefault();
            var currentTechRequests = _context.MaintenanceRequest.Where(r => r.MaintanenceTechId == currentTech.MaintenanceTechId && r.MaintenanceStatus == "In Progress").ToList();
            return View(currentTechRequests);
        }

        // GET: MaintenanceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequest
                .Include(m => m.resident)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Create
        public IActionResult Create()
        {
            ViewData["residentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId");
            return View();
        }

        // POST: MaintenanceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,isComplete,MaintenanceStatus,Message,Video,filePath,residentId,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                var currentResident = _context.Resident.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
                maintenanceRequest.MaintenanceStatus = "Pending";
                maintenanceRequest.ResidentId = currentResident.ResidentId;
                _context.Add(maintenanceRequest);
                _context.SaveChanges();
                currentResident.maintenanceRequestId = maintenanceRequest.RequestId;
                _context.Update(currentResident);
                await _context.SaveChangesAsync();
                return View("VideoUpload");
            }
            ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = _context.MaintenanceRequest.Where(m=>m.RequestId == id).FirstOrDefault();
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,isComplete,MaintenanceStatus,Message,filePath,Video,ResidentId,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
            maintenanceRequest.RequestId = id;
            if (id != maintenanceRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceRequestExists(maintenanceRequest.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MyRequests", "MaintenanceTechs");
            }
            ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

        public IActionResult Accept(int id)
        {

            var maintenanceRequest = _context.MaintenanceRequest.Where(m=>m.RequestId == id).FirstOrDefault();
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            //ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id, [Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,MaintenanceStatus,Message,filePath,Video,ResidentId,tech,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
            if (id != maintenanceRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                    var currentTech = _context.MaintenanceTech.Where(m => m.ApplicationUserId == currentUserId).FirstOrDefault();
                    maintenanceRequest.tech = currentTech;
                    maintenanceRequest.MaintanenceTechId = currentTech.MaintenanceTechId;
                    maintenanceRequest.MaintenanceStatus = "In Progress";
                    _context.Update(maintenanceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceRequestExists(maintenanceRequest.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Requests", "MaintenanceTechs");
            }
            ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequest
                .Include(m => m.resident)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceRequest = await _context.MaintenanceRequest.FindAsync(id);
            _context.MaintenanceRequest.Remove(maintenanceRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceRequestExists(int id)
        {
            return _context.MaintenanceRequest.Any(e => e.RequestId == id);

        }

        public IActionResult CompleteRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var maintenanceRequest = _context.MaintenanceRequest.Where(m=>m.RequestId == id).FirstOrDefault();
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            return View(maintenanceRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteRequest(int id, [Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,isComplete,MaintenanceStatus,Video,FeedbackMessage,ResidentId,tech,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
            if (id != maintenanceRequest.RequestId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var currentTech = _context.MaintenanceTech.Where(m => m.MaintenanceTechId == maintenanceRequest.MaintanenceTechId).FirstOrDefault();
                    maintenanceRequest.IsComplete = true;
                    maintenanceRequest.ActualCompletionDate = DateTime.Now;
                    maintenanceRequest.MaintenanceStatus = "Complete";
                    currentTech.TotalRequestCompletions++;
                    //currentTech.TotalTimeSpan += (maintenanceRequest.ActualCompletionDate - maintenanceRequest.EstimatedCompletionDate);
                    //currentTech.AvgTimeSpan = currentTech.TotalTimeSpan / currentTech.TotalRequestCompletions;
                    _context.Update(maintenanceRequest);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceRequestExists(maintenanceRequest.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MyRequests", "MaintenanceTechs");
            }
            ViewData["ResidentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.ResidentId);
            return View(maintenanceRequest);
        }

    }
}
