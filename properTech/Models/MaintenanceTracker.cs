using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class MaintenanceTracker
    {
        [Key]
        public int TrackerId { get; set; }
        [ForeignKey("MaintenanceRequest")]
        public int RequestId { get; set; }
        public MaintenanceRequest Request { get; set; }
    }
}
