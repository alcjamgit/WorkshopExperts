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
using WorkshopExpert.Core.DomainServices;

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
                var workshopId = Guid.NewGuid();
                var workshopModel = new Workshop
                {
                    Id = workshopId,
                    Title = workshopVm.Title,
                    Type_Id = workshopVm.WorkshopType_Id,
                    Category_Id = workshopVm.Category_Id,
                };
                _db.Workshops.Add(workshopModel);

                //Add Analysis and Design record
                var analysis = new Analysis { Id = Guid.NewGuid(), Workshop_Id = workshopId };
                _db.Analyses.Add(analysis);

                //Add Summary Checklist record
                List<string> checkList = SummaryChecklistGenerator.GetWorkshopCheckList();
                foreach (var item in checkList)
                {
                    var itemCheckList = new SummaryChecklist { Workshop_Id = workshopId, Description = item, IsCompleted = false };
                    _db.SummaryChecklists.Add(itemCheckList);
                }

                //Commit changes
                _db.SaveChanges();

                workshopVm.CreatedDate = workshopModel.CreateDate;
            }
            
            return Json(new[] { workshopVm }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, WorkshopGridItemVm workshopVm)
        {

            if (workshopVm != null && ModelState.IsValid)
            {
                var devToDelete = new Workshop { Id = workshopVm.Id };

                //var devToDelete = _db.Development.Find(developmentGrid.Id);
                _db.Entry(devToDelete).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            return Json(new[] { workshopVm }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Categories = new SelectList(_db.Categories.AsEnumerable(), "Id", "Name");
            var modelId = new Guid(id);
            var model = (from w in _db.Workshops
                        where w.Id == modelId
                        select new WorkshopEditVM
                        {
                            Id = modelId,
                            Title = w.Title,
                            WorkshopType_Id = w.Type_Id,
                        }).FirstOrDefault();
            if (model != null )
            {
                model.WorkshopType = new SelectList(_db.WorkshopTypes.AsEnumerable(), "Id", "Name");
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
    }
}