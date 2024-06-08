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

        public async Task<StudentLessonListforListPage> GetStudentLessonListAsync(int userId)
        {
            var absences = await _context.Absences
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .Where(p => p.StudentId == userId)
                        .ToListAsync();

            var grades = await _context.Grades
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .Where(p => p.StudentId == userId)
                        .ToListAsync();

            var teacherIds = grades.Select(g => g.Lesson.TeacherId)
                           .Union(absences.Select(a => a.Lesson.TeacherId))
                           .Distinct()
                           .ToList();

            var teachers = await _context.Users
                    .Where(u => teacherIds.Contains(u.Id))
                    .ToDictionaryAsync(t => t.Id, t => t);

            
            var mergedDataList = grades.GroupJoin(
            absences,
            grade => grade.LessonId,
            absence => absence.LessonId,
            (grade, absenceGroup) => new MergedData
            {
                LessonId = grade.LessonId,
                LessonName = grade.Lesson.Name,
                TeacherName = teachers.ContainsKey(grade.Lesson.TeacherId) ? teachers[grade.Lesson.TeacherId].Name : "Unknown",
                TeacherSurname = teachers.ContainsKey(grade.Lesson.TeacherId) ? teachers[grade.Lesson.TeacherId].Surname : "Unknown",
                MidtermGrade = grade.MidtermGrade,
                FinalGrade = grade.FinalGrade,
                AbsenceCount = absenceGroup.Sum(a => a.Count)
            }).ToList();

            return new StudentLessonListforListPage
            {
                MergedDataList = mergedDataList
            };
        }
    }
}