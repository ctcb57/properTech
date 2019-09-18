using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }
        [Display(Name = "Building Name")]
        public string BuildingName { get; set; }
        public Address Address { get; set; }
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [ForeignKey("ManagerId")]
        public int ManagerId { get; set; }
    }
}
