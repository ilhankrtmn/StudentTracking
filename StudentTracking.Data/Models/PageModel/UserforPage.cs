using StudentTracking.Data.EntityFramework.Entities;

namespace StudentTracking.Data.Models.PageModel
{
    public class UserforPage
    {
        public User User { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}