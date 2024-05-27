using StudentTracking.Data.Enums;

namespace StudentTracking.Data.EntityFramework.Entities
{
    public class GameTransaction
    {
        public int EntryID { get; set; }
        public int? CustomerID { get; set; }
        public int? GameID { get; set; }
        public int? GameConfigurationID { get; set; }
        public string InputWord { get; set; }
        public DateTime CreateDate { get; set; }
        public GameTransactionEnum Status { get; set; }
    }
}
