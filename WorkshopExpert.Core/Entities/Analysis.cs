using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopExpert.Core.DomainServices;

namespace WorkshopExpert.Core.Entities
{
    public class Analysis
    {
        public Guid Id { get; set; }
        public Guid Workshop_Id { get; set; }
        public string ProposedWorkshopTitle { get; set; }
        public decimal  DurationHours { get; set; }
        public string AttendeeProfile { get; set; }
        public string ProblemToOvercome { get; set; }
        public string NeedToFulfill { get; set; }

        [ForeignKey("Workshop_Id")]
        public virtual Workshop Workshop { get; set; }
    }
}
