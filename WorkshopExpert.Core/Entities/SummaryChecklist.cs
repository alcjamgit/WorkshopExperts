using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Core.Entities
{
    public class SummaryChecklist
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid Workshop_Id { get; set; }

        [ForeignKey("Workshop_Id")]
        public virtual Workshop Workshop { get; set; }

    }
}
