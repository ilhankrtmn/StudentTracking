using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTracking.App.Models
{
    public class UserLoginBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterBody
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int CityID { get; set; }
        public string Password { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
    }
}
