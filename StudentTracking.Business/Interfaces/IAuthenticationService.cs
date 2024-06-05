using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<int> CheckCustomerLogin(string email, string password);
        Task<bool> SendOtp(SendOtpRequestDto requestDto);
        Task<bool> CheckOtp(CheckOtpRequestDto requestDto);
        Task<bool> ResetPassword(ResetPasswordRequestDto requestDto);
        Task<User> GetUser(string email);
        Task<User> GetUser(int Id);
    }
}
