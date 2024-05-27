namespace StudentTracking.Data.Models
{
    public class CustomerSaveTransactionDto
    {
        public int CustomerID { get; set; }
        public int GameTransactionID { get; set; }
        public int GameID { get; set; }
        public string TransactionDetail { get; set; }
    }
}
