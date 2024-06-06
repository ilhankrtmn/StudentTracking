using StudentTracking.Data.EntityFramework.Entities;
using System.Web.Mvc;

namespace StudentTracking.Data.Models.PageModel
{
    public class LessonforPage
    {
        public Lesson Lesson { get; set; }
        public List<SelectListItem> TeacherList { get; set; }
    }
}