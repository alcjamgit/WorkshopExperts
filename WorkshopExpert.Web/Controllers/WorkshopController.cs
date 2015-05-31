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
    [Authorize]
    public class WorkshopController : Controller
    {
        private readonly ApplicationDbContext _db;
        public WorkshopController()
        {

            _db = new ApplicationDbContext(new CurrentUserService(System.Web.HttpContext.Current.User.Identity));
        }
        public ActionResult Index()
        {
            ViewBag.Categories = new SelectList(_db.Categories.AsEnumerable(), "Id", "Name");
            ViewBag.WorkshopTypes = new SelectList(_db.WorkshopTypes.AsEnumerable(), "Id", "Name");
            return View();
        }
        
        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {

            var model = from w in _db.Workshops
                        select new WorkshopGridItemVm
                        {
                            Id = w.Id,
                            Title = w.Title,
                            WorkshopType_Id = w.Type_Id,
                            Category_Id = w.Category_Id,
                            CreatedDate = DbFunctions.TruncateTime(w.CreateDate),

                        };
            return Json(model.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, WorkshopGridItemVm workshopVm)
        {
            if (workshopVm != null && ModelState.IsValid)
            {
                var workshopModel = new Workshop
                {
                    Id = Guid.NewGuid(),
                    Title = workshopVm.Title,
                    Type_Id = workshopVm.WorkshopType_Id,
                    Category_Id = workshopVm.Category_Id,
                };
                _db.Workshops.Add(workshopModel);
                _db.SaveChanges();
            }

            return Json(new[] { workshopVm }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Categories = new SelectList(_db.Categories.AsEnumerable(), "Id", "Name");
            ViewBag.WorkshopTypes = new SelectList(_db.WorkshopTypes.AsEnumerable(), "Id", "Name");
            var modelId = new Guid(id);
            var model = _db.Workshops.Where(w => w.Id == modelId).FirstOrDefault();
            return View(model);
        }
    }
}