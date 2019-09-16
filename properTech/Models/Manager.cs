using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Manager
   {
       [Key]
       public int Id { get; set; }
       [Display(Name ="First Name")]
       public string firstName { get; set; }
       [Display(Name = "Last Name")]
       public string lastName { get; set; }
       [Display(Name = "Current Residents")]
       public List<Resident> residentsList { get; set; }


       [ForeignKey("Property")]
        public int propertyId { get; set; }
        public Property property { get; set; }

       [ForeignKey("Users")]
        public int residentId { get; set; }
        public ApplicationUser user { get; set; }
   }
}
