using StudentTracking.Data.Models;

namespace StudentTracking.Business.Interfaces
{
    public interface IEmailService
    {
        void SendMail(SendMailRequestDto requestDto);
        Task MailContact(MailContactRequestDto requestDto);
    }
}
