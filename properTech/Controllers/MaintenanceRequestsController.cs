﻿using System;
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
    public class MaintenanceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MaintenanceRequest_1.Include(m => m.resident);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MaintenanceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequest_1
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
        public async Task<IActionResult> Create([Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,isComplete,MaintenanceStatus,FeedbackMessage,residentId,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["residentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.residentId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequest_1.FindAsync(id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            ViewData["residentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.residentId);
            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,DateOfRequest,EstimatedCompletionDate,ActualCompletionDate,isComplete,MaintenanceStatus,FeedbackMessage,residentId,MaintanenceTechId")] MaintenanceRequest maintenanceRequest)
        {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["residentId"] = new SelectList(_context.Resident, "ResidentId", "ResidentId", maintenanceRequest.residentId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequest_1
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
            var maintenanceRequest = await _context.MaintenanceRequest_1.FindAsync(id);
            _context.MaintenanceRequest_1.Remove(maintenanceRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceRequestExists(int id)
        {
            return _context.MaintenanceRequest_1.Any(e => e.RequestId == id);
        }
    }
}
