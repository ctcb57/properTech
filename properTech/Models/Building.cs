using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Building
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Building Name")]
        public string buildingName { get; set; }
        public Address address { get; set; }
        public Unit unit { get; set; }
    }
}
