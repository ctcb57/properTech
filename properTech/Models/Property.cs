using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }
        public Address Address { get; set; }
        [ForeignKey("Manager Id")]
        public int ManagerId { get; set; }

    }
}
