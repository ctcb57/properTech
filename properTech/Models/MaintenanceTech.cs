using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class MaintenanceTech
    {
        [Key]
        public int MaintenanceTechId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "User Id")]
        public string ApplicationUserId { get; set; }
<<<<<<< HEAD
        public int TotalRequestCompletions { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
        public TimeSpan AvgTimeSpan { get; set; }
=======
        [Display(Name = "Average Deviation")]
        public int AverageDeviation { get; set; }
>>>>>>> 8d264005f43f73e2df5788feab38ada6a215810b
    }
}
