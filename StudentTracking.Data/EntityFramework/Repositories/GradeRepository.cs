using Microsoft.EntityFrameworkCore;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class GradeRepository(StudentTrackingContext context) : EfCoreRepositoryBase<Grade>(context), IGradeRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context = context;

        public async Task<List<Grade>> GetGradesAsync(GetGradeListRequestDto requestDto)
        {
            return await _context.Grades
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .Where(p => p.Lesson.TeacherId == requestDto.UserId && p.LessonId == requestDto.LessonId)
                        .ToListAsync();
        }
    }
}