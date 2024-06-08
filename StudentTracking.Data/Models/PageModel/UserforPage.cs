using StudentTracking.Data.EntityFramework.Entities;
using System.Web.Mvc;

namespace StudentTracking.Data.Models.PageModel
{
    public class UserforPage
    {
        public User User { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public List<SelectListItem> StudenList { get; set; }
    }
}