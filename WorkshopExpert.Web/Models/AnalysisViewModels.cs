using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Web.Models
{
    public class AnalysisEditViewModel
    {
        public Guid Id { get; set; }
        public Guid Workshop_Id { get; set; }

        public string ProposedWorkshopTitle { get; set; }
        [UIHint("TimePicker")]
        public decimal DurationHours { get; set; }
        public string AttendeeProfile { get; set; }
        public string ProblemToOvercome { get; set; }
        public string NeedToFulfill { get; set; }

    }

}
