using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace properTech.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public FileUploadController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            //long size = files.Sum(f => f.Length);
            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            foreach (var formFile in files)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "videos");
                var fullPath = Path.Combine(uploads, GetUniqueFileName(formFile.FileName));
                formFile.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            
            return Ok(new { count = files.Count, /*size, */filePath });
            //return files;
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