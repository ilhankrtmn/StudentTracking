namespace StudentTracking.Data.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
