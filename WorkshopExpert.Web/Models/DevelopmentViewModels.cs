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
    public class DevelopmentGridVm
    {
        public Guid Id { get; set; }
        [UIHint("Integer")]
        public int Order { get; set; }
        [DisplayName("Topic Title")]
        public string TopicTitle { get; set; }

        [UIHint("GridForeignKey")][DisplayName("Time Slot")]
        public int TimeSlot_Id { get; set; }

        [UIHint("GridForeignKey")][DisplayName("Delivery")]
        public int DeliveyMethod_Id { get; set; }

        [DisplayName("Duration (hh:mm)")]
        public TimeSpan? Duration { get; set; }
        public Guid Workshop_Id { get; set; }

    }
}
