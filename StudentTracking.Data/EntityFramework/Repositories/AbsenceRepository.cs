using Microsoft.EntityFrameworkCore;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class AbsenceRepository(StudentTrackingContext context) : EfCoreRepositoryBase<Absence>(context), IAbsenceRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context = context;

        public async Task<List<Absence>> GetAbsenceAsync(GetAbsenceListRequestDto requestDto)
        {
            var absences = Queryable()
                        .AsNoTracking()
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .Where(p => p.LessonId == requestDto.LessonId);

            if (requestDto.UserId != null)
            {
                absences = absences.Where(p => p.Lesson.TeacherId == requestDto.UserId);
            }

            return await absences.ToListAsync();
        }

        public async Task<List<Absence>> GetAllAbsenceAsync()
        {
            return await Queryable()
                        .AsNoTracking()
                        .Include(p => p.Lesson)
                        .Include(p => p.User)
                        .ToListAsync();
        }
    }
}
