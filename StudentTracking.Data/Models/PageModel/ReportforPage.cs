using StudentTracking.Data.EntityFramework.Entities;
using System.Web.Mvc;

namespace StudentTracking.Data.Models.PageModel
{
    public class ReportforPage
    {
        public int ReportTypeId { get; set; }
        public string ReportTypeName { get; set; }
        public int? LessonId { get; set; }
        public List<SelectListItem> LessonList { get; set; }
        public int? SituationTypeId { get; set; }
        public string SituationTypeName { get; set; }
        public int? SearchParameter { get; set; }
        public int? StatusTypeId { get; set; }
        public string StatusTypeName { get; set; }

        public List<Grade> Grades { get; set; }
        public List<Absence> Absences { get; set; }
    }
}