using StudentTracking.Data.EntityFramework.Entities;

namespace StudentTracking.Data.Models
{
    public class ParticipateGamePage
    {
        public GameConfiguration GameConfiguration { get; set; }
        public Game Game { get; set; }
    }
}
