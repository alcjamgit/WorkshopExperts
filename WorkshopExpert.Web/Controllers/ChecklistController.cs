using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkshopExpert.Core.Entities;
using WorkshopExpert.Infrastructure.BLL.ApplicationServices;
using WorkshopExpert.Infrastructure.DAL;
using Kendo.Mvc.Extensions;
using WorkshopExpert.Web.Models;
using System.Data.Entity;

namespace WorkshopExpert.Web.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ChecklistController()
        {
            _db = new ApplicationDbContext(new CurrentUserService(System.Web.HttpContext.Current.User.Identity));
        }

        // GET: Checklist
        public PartialViewResult Index(string id)
        {
            ViewBag.WorshopId = id;
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string id) 
        {
            var modelId = new Guid(id);
            var model = from s in _db.SummaryChecklists
                         where s.Workshop_Id == modelId
                         select  new ChecklistReadVm {
                            Id = s.Id,
                            Description = s.Description,
                            IsCompleted = s.IsCompleted,
                            Workshop_Id = s.Workshop_Id,
                         };

            return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, IEnumerable<ChecklistReadVm> checklistGrid)
        {

            if (checklistGrid != null && ModelState.IsValid)
            {
                if (checklistGrid.Any())
                {
                    foreach (var checkList in checklistGrid)
                    {
                        var checkListModel = new SummaryChecklist
                        {
                            Id = checkList.Id,
                            IsCompleted = checkList.IsCompleted,
                            Workshop_Id = checkList.Workshop_Id,
                        };

                        _db.SummaryChecklists.Attach(checkListModel);
                        _db.Entry(checkListModel).Property(c => c.IsCompleted).IsModified = true;
                    }
                }

                _db.SaveChanges();
            }
            return Json(new[] { checklistGrid }.ToDataSourceResult(request, ModelState));
        }
    }
}