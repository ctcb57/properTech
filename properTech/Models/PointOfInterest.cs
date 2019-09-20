using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class PointOfInterest
    {
        [Key]
        public int PointOfInterestId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TypeOfBusiness { get; set; }
        public string PhoneNumber { get; set; }
    }
}
