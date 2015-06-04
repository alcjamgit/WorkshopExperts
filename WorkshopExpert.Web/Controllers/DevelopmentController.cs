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
    public class DevelopmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DevelopmentController()
        {
            _db = new ApplicationDbContext(new CurrentUserService(System.Web.HttpContext.Current.User.Identity));
        }

        
        public ActionResult IndexByWorkshopId(string id)
        {
            ViewBag.WorkshopId = id;
            ViewBag.TimeSlots = new SelectList(_db.TimeSlots.AsEnumerable(), "Id", "Name");
            ViewBag.DeliveryMethods = new SelectList(_db.DeliveryMethods.AsEnumerable(), "Id", "Name");
            return PartialView();
        }
        
        public ActionResult Index_Read([DataSourceRequest] DataSourceRequest request, string id)
        {

            var modelId = new Guid(id);
            var model = from d in _db.Development
                        where d.Workshop_Id == modelId
                        select new DevelopmentGridVm 
                        {
                            Id = d.Id,
                            TopicTitle = d.TopicTitle,
                            TimeSlot_Id = d.TimeSlot_Id,
                            Order = d.Order,
                            Workshop_Id = d.Workshop_Id,
                            DeliveyMethod_Id = d.DeliveyMethod_Id,
                            Duration = DbFunctions.CreateTime(0, 1 ,0f)
                        };
            return Json(model.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, DevelopmentGridVm developmentGrid)
        {
            if (developmentGrid != null && ModelState.IsValid)
            {
                var workshopModel = new Development
                {
                    Id = Guid.NewGuid(),
                    TopicTitle = developmentGrid.TopicTitle,
                    DeliveyMethod_Id = developmentGrid.DeliveyMethod_Id,
                    TimeSlot_Id = developmentGrid.TimeSlot_Id,
                    Workshop_Id = developmentGrid.Workshop_Id,
                    Order = developmentGrid.Order,
                };
                _db.Development.Add(workshopModel);
                _db.SaveChanges();
            }

            return Json(new[] { developmentGrid }.ToDataSourceResult(request, ModelState));
        }


    }
}