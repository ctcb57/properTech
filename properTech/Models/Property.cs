using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Property Name")]
        public string propertyName { get; set; }
        public Building building { get; set; }
        public Address address { get; set; }

    }
}
