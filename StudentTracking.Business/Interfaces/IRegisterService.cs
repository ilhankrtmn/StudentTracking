using StudentTracking.Data.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTracking.Business.Interfaces
{
    public interface IRegisterService
    {
        Task InsertCustomer(Customer customer);
        Task<int> CheckCustomerLogin(string email, string password);
        Task<bool> CheckRegisterMail(string email);
        Task<IEnumerable<SelectListItem>> GetCitiesAsync();
        Task<bool> UpdateCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerID);
    }
}
