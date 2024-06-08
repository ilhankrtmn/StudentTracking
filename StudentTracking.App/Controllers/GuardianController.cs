using Microsoft.AspNetCore.Mvc;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.App.Controllers
{
    public class GuardianController : Controller
    {
        private readonly IUserService _userService;

        public GuardianController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> MailList()
        {
            MailListforListPage mailListforListPage = new MailListforListPage();

            mailListforListPage = await _userService.GetGuardianMailListAsync(SessionContext.GetInt("UserId"));

            return View(mailListforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> MailDetail(int mailId)
        {
            MailListforPage mailListforPage = new MailListforPage();

            mailListforPage.OutgoingMail = await _userService.GetGuardianMailDetailAsync(SessionContext.GetInt("UserId"), mailId);

            return View(mailListforPage);
        }
    }
}