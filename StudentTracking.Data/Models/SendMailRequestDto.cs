namespace StudentTracking.Data.Models
{
    public class SendMailRequestDto
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? From { get; set; }
    }
}