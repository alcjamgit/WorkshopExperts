using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WorkshopExpert.Web.Models
{
    public class AnalysisEditViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [HiddenInput]
        public Guid Workshop_Id { get; set; }

        [DisplayName("Proposed Workshop Title")]
        public string ProposedWorkshopTitle { get; set; }

        [DisplayName("Estimated Length of Workshop")]
        public decimal DurationHours { get; set; }

        [DisplayName("Who Should Attend?")]
        public string AttendeeProfile { get; set; }

  
        [DisplayName("In one detailed paragraph, please describe the problem in the industry that this workshop helps to overcome")]
        public string ProblemToOvercome { get; set; }

        [DisplayName("In one detailed paragraph, please describe the need that this workshop will fulfill")]
        public string NeedToFulfill { get; set; }

    }

}
