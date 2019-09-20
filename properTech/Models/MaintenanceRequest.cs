using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class MaintenanceRequest
    {
        [Key]
        public int RequestId { get; set; }
        [Display(Name ="Date of Request")]
        [DataType(DataType.Date)]
        public DateTime DateOfRequest { get; set; }
        [Display(Name ="Estimated Date of Completion")]
        [DataType(DataType.Date)]
        public DateTime EstimatedCompletionDate { get; set; }
        [Display(Name = "Actual Date of Completion")]
        public DateTime ActualCompletionDate { get; set; }
        [Display(Name ="Maintenance Complete")]
        public bool isComplete { get; set; }
        [Display(Name = "Current Maintenance Status")]
        public string MaintenanceStatus { get; set; }
        [Display(Name = "Messages")]
        public string Message { get; set; }
        public string filePath { get; set; }
        [NotMapped]
        public IFormFile Video { get; set; }

        [ForeignKey("Resident")]
        public int ResidentId { get; set; }
        public Resident resident { get; set; }

        [ForeignKey("MaintenanceTech")]
        public int MaintanenceTechId { get; set; }
        public MaintenanceTech tech { get; set; }
    }
}
