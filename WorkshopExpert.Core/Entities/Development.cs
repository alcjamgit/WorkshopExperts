using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Core.Entities
{
    public class Development
    {
        public Guid Id { get; set; }
        public int TimeSlot_Id { get; set; }
        public int Order { get; set; }
        public string TopicTitle { get; set; }
        public int DeliveyMethod_Id { get; set; }
        public long Duration { get; set; }
        public Guid Workshop_Id { get; set; }

        [ForeignKey("DeliveyMethod_Id")]
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        [ForeignKey("Workshop_Id")]
        public virtual Workshop Workshop { get; set; }
        [ForeignKey("TimeSlot_Id")]
        public virtual TimeSlot TimeSlot { get; set; }
    }
}
