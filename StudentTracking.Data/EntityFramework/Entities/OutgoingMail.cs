namespace StudentTracking.Data.EntityFramework.Entities
{
    public class OutgoingMail
    {
        public int Id { get; set; }
        public int? SendUserId { get; set; }
        public int RecipientUserId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}