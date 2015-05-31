using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using WorkshopExpert.Core.Entities;
using WorkshopExpert.Infrastructure.DAL;
using WorkshopExpert.Web.Models;
using WorkshopExpert.Infrastructure.BLL.ApplicationServices;
using System.Data.Entity;

namespace WorkshopExpert.Web.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        private readonly ApplicationDbContext _db;
        public DesignController()
        {
            _db = new ApplicationDbContext(new CurrentUserService(System.Web.HttpContext.Current.User.Identity));
        }

    }
}