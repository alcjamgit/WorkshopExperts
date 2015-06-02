using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopExpert.Core.Entities;
using WorkshopExpert.Infrastructure.DAL;

namespace WorkshopExpert.Infrastructure.DomainServices
{
    public class SummaryChecklistService
    {
        private readonly ApplicationDbContext _db;
        public SummaryChecklistService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddChecklistItems(Guid workshopId)
        {
            IEnumerable<SummaryChecklist> checklistItems = new List<SummaryChecklist>
            {
                new SummaryChecklist { Workshop_Id = workshopId, Description = "Have I Identified a need for the workshop and described it clearly on the workshop lesson blueprint?" },
                new SummaryChecklist { Workshop_Id = workshopId, Description = "Have I listed at least 4-5 learning outcomes for each hour of workshop seat time?" },
                new SummaryChecklist { Workshop_Id = workshopId, Description = "Have I included several instructional methods during the planning and design of my workshop?" },
                new SummaryChecklist { Workshop_Id = workshopId, Description = "Have I developed participant handouts and other materials for the workshop and submitted them to the In.Acuity?" }
            };
            _db.SummaryChecklists.AddRange(checklistItems);
        }
    }
}
