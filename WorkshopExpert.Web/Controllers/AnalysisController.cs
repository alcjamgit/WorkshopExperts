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
    public class AnalysisController : Controller
    {
        // GET: Design
        private readonly ApplicationDbContext _db;

        public AnalysisController()
        {
            _db = new ApplicationDbContext(new CurrentUserService(System.Web.HttpContext.Current.User.Identity));
        }

        public ActionResult GetByAnalysisByWorkshopId(string id)
        {
            var modelId = new Guid(id);
            var model = (from a in _db.Analyses
                        where a.Workshop_Id == modelId
                        select new AnalysisEditViewModel {
                            Id = a.Id,
                            Workshop_Id = a.Workshop_Id,
                            DurationHours = a.DurationHours,
                            AttendeeProfile = a.AttendeeProfile,
                            ProposedWorkshopTitle = a.ProposedWorkshopTitle,
                            ProblemToOvercome = a.ProblemToOvercome,
                            NeedToFulfill = a.NeedToFulfill
                        }).FirstOrDefault();

            return PartialView(model);
        }

    }
}