using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public Address address { get; set; }
        [Display(Name = "Unit Number")]
        public int unitNumber { get; set; }
        [Display(Name = "Number of Bedrooms")]
        public int roomCount { get; set; }
        [Display(Name = "Number of Bathrooms")]
        public int bathroomCount { get; set; }
        [Display(Name = "Unit Size (ft2)")]
        public int squareFootage { get; set; }
        [Display(Name ="Monthly Rent Charge")]
        public double monthlyRent { get; set; }
        public bool isOccupied { get; set; }

    }
}
