using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        ApplicationDbContext _context;
        public FileUploadController(IHostingEnvironment environment, ApplicationDbContext context)
        {
            _context = context;
            hostingEnvironment = environment;
        }
        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var resident = _context.Resident.Where(r => r.ApplicationUserId == currentUserId).FirstOrDefault();
            var currentMaintenanceRequest = _context.MaintenanceRequest.Where(m=> m.confirmationNumber == resident.maintenanceRequestId).FirstOrDefault();
            var filePath = Path.GetTempFileName();
            foreach (var formFile in files)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "videos");
                var fullPath = Path.Combine(uploads, GetUniqueFileName(formFile.FileName));
                formFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                currentMaintenanceRequest.filePath = fullPath;
                _context.SaveChanges();
            }
            return Ok(new { count = files.Count, filePath });
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}