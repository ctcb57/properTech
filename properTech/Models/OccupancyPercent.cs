using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class OccupancyPercent
    {
        [Key]
        public int Id { get; set; }
        public string Month { get; set; }
        public double OccupancyPercentage { get; set; }
    }
}
