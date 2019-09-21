using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class UnassignedUsers
    {
        [Key]
        public int UnassignedId { get; set; }
        public string Email { get; set; }
        [ForeignKey("UserId")]
        public string ApplicationUserId { get; set; }
    }
}
