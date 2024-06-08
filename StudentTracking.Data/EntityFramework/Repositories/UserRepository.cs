using Microsoft.EntityFrameworkCore;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class UserRepository : EfCoreRepositoryBase<User>, IUserRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context;

        public UserRepository(StudentTrackingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudenttoLesson>> GetStudentList(int lessonId)
        {
            var absencesStudent = await _context.Absences.Where(p => p.LessonId == lessonId).Select(p => p.StudentId).ToListAsync();
            var gradeStudent = await _context.Absences.Where(p => p.LessonId == lessonId).Select(p => p.StudentId).ToListAsync();

            return await _context.Users.Where(p => p.UserTypeId == (int)UserTypes.Student && !absencesStudent.Contains(p.Id) && !gradeStudent.Contains(p.Id))
                        .Select(p => new StudenttoLesson
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Surname = p.Surname,
                            StudentNumber = p.StudentNumber.Value,
                            Status = false
                        })
                        .ToListAsync();
        }
    }
}