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
                        return RedirectToAction("LessonList", "Admin");
                    case 3:
                        return RedirectToAction("UserList", "Admin");
                    case 4:
                        return RedirectToAction("StudentLessonList", "Student");
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

            return (response == true) ? RedirectToAction("Login", "Authentication") : RedirectToAction("ResetPassword", "Authentication");
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
