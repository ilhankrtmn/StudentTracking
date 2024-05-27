namespace StudentTracking.Data.EntityFramework.Entities
{
    public class CustomerTransaction
    {
        public int TransactionId { get; set; }
        public int CustomerID { get; set; }
        public int GameTransactionID { get; set; }
        public int GameID { get; set; }
        public int TransactionType { get; set; }
        public string TransactionDetail { get; set; }
        public DateTime Createdate { get; set; }
    }
}
