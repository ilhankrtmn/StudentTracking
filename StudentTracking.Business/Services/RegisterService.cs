using StudentTracking.Business.Interfaces;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentTracking.Business.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public RegisterService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task InsertCustomer(Customer customer)
        {
            _customerRepository.Add(customer);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<int> CheckCustomerLogin(string email, string password)
        {
            var customer = await _customerRepository.FindAsync(p => p.Email == email && p.Password == password && p.Status == 1);
            if (customer == null)
            {
                customer = new Customer { CustomerID = 0 };
            }

            return customer.CustomerID;
        }

        public async Task<bool> CheckRegisterMail(string email)
        {
            var customer = await _customerRepository.FindAsNoTrackingAsync(p => p.Email == email && p.Status == 1);
            return customer == null;
        }

        public async Task<IEnumerable<SelectListItem>> GetCitiesAsync()
        {
            var cities = await _customerRepository.GetCityDataAsync();

            return cities.Select(city => new SelectListItem
            {
                Value = city.CityID.ToString(),
                Text = city.City
            });
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Customer> GetCustomer(int customerID)
        {
            return await _customerRepository.FindAsNoTrackingAsync(p => p.CustomerID == customerID);
        }
    }
}
