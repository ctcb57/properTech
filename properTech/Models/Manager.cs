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
       public string FirstName { get; set; }
       [Display(Name = "Last Name")]
       public string LastName { get; set; }
       [Display(Name = "Current Residents")]

        public int ResidentId { get; set; }
   }
}
