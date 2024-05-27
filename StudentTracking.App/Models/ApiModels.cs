using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTracking.App.Models
{
    public class CustomerLoginBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CustomerRegisterBody
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int CityID { get; set; }
        public string Password { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
    }

    public class ParticipateGameBody
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int GameID { get; set; }
        public string InputWord { get; set; }
    }
}
