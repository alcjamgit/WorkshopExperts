using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Core.DomainServices
{
    public static class SummaryChecklistGenerator
    {
        public static List<string> GetWorkshopCheckList() 
        {
            var checkList = new List<string>
            {
                "Have I Identified a need for the workshop and described it clearly on the Workshop Lesson Blueprint?",
                "Have I listed at least 4-5 Learning Outcomes (Objectives) for each hour of workshop seat time?",
                "Have I used Bloom's Taxonomy to develop Learning Outcomes that reach deeper cognitive levels and skills?",
                "Have I included several instructional methods during the workshop so as to meet Bloom's instructional standards?",
                "Have I used general Principles of Adult Learning during the planning and any design of my workshop?",
                "Have I completed my Workshop Lesson Blueprint and returned it to In.Acuity?",
                "Have I planned methods to assess whether the participants have met my learning objectives?",
                "Have I created the workshop session presentations and supplementary material and submitted them to In.Acuity?",
                "Have I Developed the participant handouts and other materials for the workshop and submitted them to In.Acuity?",
            };
            return checkList;
        }

    }
}
