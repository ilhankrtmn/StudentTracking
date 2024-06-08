namespace StudentTracking.Data.Models
{
    public class MailContactRequestDto
    {
        public int SendUserId { get; set; }
        public int RecipientUserId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}