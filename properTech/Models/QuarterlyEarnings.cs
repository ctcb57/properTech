using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class QuarterlyEarnings
    {
        [Key]
        public int Id { get; set; }
        public string Quarter { get; set; }
        public double Earnings { get; set; }
    }
}
