using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracking.App.Models;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.Models;

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
            //UserRegisterBody customerRegisterBody = new UserRegisterBody();

            //customerRegisterBody.CityList = await _registerService.GetCitiesAsync();
            //return View(customerRegisterBody);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterBody customerRegisterBody)
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
        public async Task<IActionResult> Login(UserLoginBody userLoginBody)
        {
            var Id = await _authenticationService.CheckCustomerLogin(userLoginBody.Email, userLoginBody.Password);
            if (Id != 0)
            {
                var user = await _authenticationService.GetUser(Id);
                SessionContext.SetInt("UserId", Id);
                SessionContext.SetInt("UserTypeId", user.UserTypeId);
                switch (user.UserTypeId)
                {
                    case 1:
                        return RedirectToAction("UserList", "Admin");
                    case 2:
                        return RedirectToAction("UserList", "Admin");
                    case 3:
                        return RedirectToAction("UserList", "Admin");
                    case 4:
                        return RedirectToAction("UserList", "Admin");
                    default:
                        return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SendOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(SendOtpRequestDto requestDto)
        {
            var response = await _authenticationService.SendOtp(requestDto);
            SessionContext.SetString("email", requestDto.Email);

            return (response == true) ? RedirectToAction("CheckOtp", "Authentication") : RedirectToAction("SendOtp", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> CheckOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckOtp(CheckOtpRequestDto requestDto)
        {
            requestDto.Email = SessionContext.GetString("email");
            var response = await _authenticationService.CheckOtp(requestDto);
            var user = await _authenticationService.GetUser(requestDto.Email);
            SessionContext.SetInt("userId", user.Id);

            return (response == true) ? RedirectToAction("ResetPassword", "Authentication") : RedirectToAction("CheckOtp", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto requestDto)
        {
            requestDto.UserId = SessionContext.GetInt("userId");

            var response = await _authenticationService.ResetPassword(requestDto);
            // TODO Burada istediğim sayfa yönlendirme işlemini yapmıyor. View tarafından kaynaklı duruyor. Kontrol et.
            return (response == true) ? RedirectToAction("Login", "Authentication") : RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            SessionContext.Remove("UserId");
            SessionContext.Remove("UserTypeId");
            return RedirectToAction("Login", "Authentication");
        }
    }
}
