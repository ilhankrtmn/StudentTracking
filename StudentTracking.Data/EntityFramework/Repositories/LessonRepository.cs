using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class LessonRepository(StudentTrackingContext context) : EfCoreRepositoryBase<Lesson>(context), ILessonRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context = context;


    }
}
