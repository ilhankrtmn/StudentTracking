namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Game
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public int? GameParticipationTypeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }

    public class GameforListPage
    {
        public List<Game> Games { get; set; }
    }
}
