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
        public int ResidentId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Lease Start Date")]
        public DateTime LeaseStart { get; set; }
        [Display(Name = "Lease End Date")]
        public DateTime LeaseEnd { get; set; }
        [Display(Name = "Lease Renewed")]
        public bool RenewedLease { get; set; }

        [Display(Name ="Payment Due Date")]
        public DateTime PaymentDueDate { get; set; }
        [Display(Name = "Late Payment")]
        public bool LatePayment { get; set; }
        [Display(Name = "Current Balance Due")]
        public double Balance { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        [ForeignKey("User Id")]
        public string ApplicationUserId { get; set; }
    }
}
