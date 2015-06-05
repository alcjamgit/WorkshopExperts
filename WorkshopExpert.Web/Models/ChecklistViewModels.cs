using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkshopExpert.Web.Models
{
    public class ChecklistReadVm
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public Guid Workshop_Id { get; set; }
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

    }

}