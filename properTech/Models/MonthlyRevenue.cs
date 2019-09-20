using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class MonthlyRevenue
    {
        [Key]
        public int Id { get; set; }
        public string Month { get; set; }
        public double Revenue { get; set; }
    }
}
