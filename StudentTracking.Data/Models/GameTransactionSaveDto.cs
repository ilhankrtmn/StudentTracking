namespace StudentTracking.Data.Models
{
    public class GameTransactionSaveDto
    {
        public int CustomerID { get; set; }
        public int GameID { get; set; }
        public int GameConfigurationID { get; set; }
        public string InputWord { get; set; }
    }
}
