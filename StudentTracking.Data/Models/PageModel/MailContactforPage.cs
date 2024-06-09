using StudentTracking.Data.EntityFramework.Entities;
using System.Web.Mvc;

namespace StudentTracking.Data.Models.PageModel
{
    public class MailContactforPage
    {
        public OutgoingMail OutgoingMail { get; set; }
        public List<SelectListItem> GuardianList { get; set; }
        public bool Status { get; set; }
    }
}