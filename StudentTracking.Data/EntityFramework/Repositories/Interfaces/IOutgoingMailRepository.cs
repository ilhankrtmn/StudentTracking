using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface IOutgoingMailRepository : IRepositoryBase<OutgoingMail>
    {
        Task InsertUserEmailOtpAsync(int UserId, int pincode);
        Task<bool> SendOtpMailAsync(MailContactRequestDto requestDto);
        Task<int> CheckUserEmailIdAsync(int userId, int pincode);
        Task UpdateUserEmailOtpAsync(int emailId);
    }
}
