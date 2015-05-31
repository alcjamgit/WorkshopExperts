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
    public class WorkshopMainIndexVm
    {
      
        public Guid Id { get; set; }
        public string Title { get; set; }
        [UIHint("GridForeignKey")]
        public Guid Category_Id { get; set; }
        [UIHint("GridForeignKey")]
        public int WorkshopType_Id { get; set; }
        public DateTime CreatedDate { get; set; }

    }
    public class WorkshopCreateVM
    {

        public string Title { get; set; }
        public Guid Category_Id { get; set; }
        public int WorkshopType_Id { get; set; }
    }

    public class WorkshopGridItemVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        [UIHint("GridForeignKey")]
        [DisplayName("Category")]
        public Guid Category_Id { get; set; }
        [UIHint("GridForeignKey")]
        [DisplayName("Type")]
        public int WorkshopType_Id { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
