using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class VacancyTracker
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Unit")]
        public int unitId { get; set; }
        public Unit unit { get; set; }
    }
}
