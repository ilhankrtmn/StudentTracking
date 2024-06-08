using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<List<StudenttoLesson>> GetStudentList(int lessonId);
    }
}