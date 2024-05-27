namespace StudentTracking.Data.EntityFramework.Entities
{
    public class GameConfiguration
    {
        public int ID { get; set; }
        public int? GameID { get; set; }
        public string Word { get; set; }
        public string? SpelledWord { get; set; }
        public string Image { get; set; }
        public string Sound { get; set; }
        public int? Status { get; set; }
    }
}
