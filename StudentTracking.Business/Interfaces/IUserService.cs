using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Core.Enums;
using StudentTracking.Data.Models.PageModel;
using System.Web.Mvc;

namespace StudentTracking.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<SelectListItem>> GetUserDataListAsync(UserTypes userTypes);
        Task<List<SelectListItem>> GetGuardianDataListAsync(UserTypes userTypes);
        Task<string> GetUserMailAsync(int userId);
        Task<List<OutgoingMail>> GetTeacherMailListAsync(int userId);
        Task<OutgoingMail> GetTeacherMailDetailAsync(int userId, int mailId);
        Task<List<StudenttoLesson>> GetStudentListAsync(int lessonId);
        Task SaveStudenttoLessonAsync(StudenttoLessonforListPage requestDto);
        Task<List<SelectListItem>> GetStudentDataListAsync(UserTypes userTypes);
        Task<StudentLessonListforListPage> GetStudentLessonListAsync(int userId);
        Task<MailListforListPage> GetGuardianMailListAsync(int guardianId);
        Task<OutgoingMail> GetGuardianMailDetailAsync(int guardianId, int mailId);
    }
}