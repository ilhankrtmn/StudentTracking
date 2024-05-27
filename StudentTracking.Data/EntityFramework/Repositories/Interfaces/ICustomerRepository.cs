using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<List<Cities>> GetCityDataAsync();
        Task AddCustomerTransaction(CustomerSaveTransactionDto customerSaveTransactionDto);
        Task<List<CustomerTransaction>> GetGameHistory(int customerID);
    }
}
