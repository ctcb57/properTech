using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class ReportView
    {
        [Key]
        public int Id { get; set; }
        public string DimensionOne { get; set; }
        public double Quantity { get; set; }
    }
}
