namespace StudentTracking.Data.EntityFramework.UnitOfWork
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly StudentTrackingContext _context;

        public EfCoreUnitOfWork(StudentTrackingContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}
