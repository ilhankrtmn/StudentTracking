using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models.PageModel;
using System.Web.Mvc;

namespace StudentTracking.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<SelectListItem>> GetUserDataListAsync(UserTypes userTypes);
        Task<string> GetUserMailAsync(int userId);
        Task<List<OutgoingMail>> GetTeacherMailListAsync(int userId);
        Task<OutgoingMail> GetTeacherMailDetailAsync(int userId, int mailId);
        Task<List<StudenttoLesson>> GetStudentListAsync(int lessonId);
        Task SaveStudenttoLessonAsync(StudenttoLessonforListPage requestDto);
    }
}