using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracking.App.Models;
using StudentTracking.Business.Interfaces;

namespace StudentTracking.App.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            //CustomerRegisterBody customerRegisterBody = new CustomerRegisterBody();

            //customerRegisterBody.CityList = await _registerService.GetCitiesAsync();
            //return View(customerRegisterBody);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterBody customerRegisterBody)
        {
            //Customer customer = new Customer();
            //customer.Name = customerRegisterBody.Name;
            //customer.Surname = customerRegisterBody.Surname;
            //customer.Email = customerRegisterBody.Email;
            //customer.CityID = customerRegisterBody.CityID;
            //customer.Password = customerRegisterBody.Password;

            //customerRegisterBody.CityList = await _registerService.GetCitiesAsync();

            //RegisterValidator registerValidator = new RegisterValidator();
            //ValidationResult validationResult = registerValidator.Validate(customer);

            //if (validationResult.IsValid)
            //{
            //    if (await _registerService.CheckRegisterMail(customerRegisterBody.Email))
            //    {
            //        await _registerService.InsertCustomer(customer);
            //        return RedirectToAction("GameList", "Customer");
            //    }
            //    else
            //    {
            //        //TODO else bloğuna düşerse yönlendirme alanını değiştir.
            //        return RedirectToAction("Login", "Customer");
            //    }
            //}
            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            //return View(customerRegisterBody);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CustomerLoginBody customerLoginBody)
        {
            return View();
            //var customerID = await _registerService.CheckCustomerLogin(customerLoginBody.Email, customerLoginBody.Password);
            //if (customerID != 0)
            //{
            //    HttpContext.Session.SetString("customerID", customerID.ToString());
            //    return RedirectToAction("GameList", "Customer");
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Customer");
            //}
        }
    }
}
