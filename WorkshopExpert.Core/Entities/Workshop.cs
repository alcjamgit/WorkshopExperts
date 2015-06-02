using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkshopExpert.Core.DomainServices;

namespace WorkshopExpert.Core.Entities
{
    public class Workshop: IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid Category_Id { get; set; }
        public int Type_Id { get; set; }

        #region IAuditable Implementation
        [StringLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(128)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion IAuditable Implementation

        [ForeignKey("Type_Id")]
        public virtual WorkshopType WorkshopType { get; set; }
        [ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser UserCreator { get; set; }
        [ForeignKey("ModifiedBy")]
        public virtual ApplicationUser UserModifier { get; set; }

        public virtual ICollection<Development> Developments { get; set; }
        public virtual ICollection<SummaryChecklist> SummaryChecklists { get; set; }
    }
}
