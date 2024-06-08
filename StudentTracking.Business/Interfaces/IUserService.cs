using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Enums;
using System.Web.Mvc;

namespace StudentTracking.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<SelectListItem>> GetUserDataListAsync(UserTypes userTypes);
        Task<string> GetUserMailAsync(int userId);
        Task<List<OutgoingMail>> GetTeacherMailListAsync(int userId);
        Task<OutgoingMail> GetTeacherMailDetailAsync(int userId, int mailId);
    }
}