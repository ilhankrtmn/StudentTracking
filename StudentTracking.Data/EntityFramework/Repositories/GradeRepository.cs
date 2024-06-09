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
            var grades = Queryable()
                        .AsNoTracking()
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .Where(p => p.LessonId == requestDto.LessonId);

            if (requestDto.UserId != null)
            {
                grades = grades.Where(p => p.Lesson.TeacherId == requestDto.UserId);
            }

            return await grades.ToListAsync();
        }

        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await Queryable()
                        .AsNoTracking()
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .ToListAsync();
        }
    }
}