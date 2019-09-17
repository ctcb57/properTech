using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public double Longitude { get; set; }
        [NotMapped]
        public double  Latitude { get; set; }
        [Display(Name ="Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}
