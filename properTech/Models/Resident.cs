using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Lease Start Date")]
        public DateTime leaseStart { get; set; }
        [Display(Name = "Lease End Date")]
        public DateTime leaseSEnd { get; set; }
        [Display(Name = "Lease Renewed")]
        public bool renewedLease { get; set; }

        [Display(Name ="Payment Due Date")]
        public DateTime paymentDueDate { get; set; }
        [Display(Name = "Late Payment")]
        public bool latePayment { get; set; }
        [Display(Name = "Current Balance Due")]
        public double balance { get; set; }

        [ForeignKey("Users")]
        public int userId { get; set; }
        public ApplicationUser user { get; set; }

        [ForeignKey("Unit")]
        public int unitId { get; set; }

        public Unit unit { get; set; }
    }
}
