using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class UserRepository : EfCoreRepositoryBase<User>, IUserRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context;

        public UserRepository(StudentTrackingContext context) : base(context)
        {
            _context = context;
        }
    }
}
