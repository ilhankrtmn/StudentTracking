using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<List<Cities>> GetCityDataAsync();
    }
}
