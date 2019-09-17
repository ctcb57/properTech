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
        public int UnitId { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Unit Number")]
        public int UnitNumber { get; set; }
        [Display(Name = "Number of Bedrooms")]
        public int RoomCount { get; set; }
        [Display(Name = "Number of Bathrooms")]
        public int BathroomCount { get; set; }
        [Display(Name = "Unit Size (ft2)")]
        public int SquareFootage { get; set; }
        [Display(Name ="Monthly Rent Charge")]
        public double MonthlyRent { get; set; }
        public bool isOccupied { get; set; }
        [ForeignKey("BuildingId")]
        public int BuildingId { get; set; }

    }
}
