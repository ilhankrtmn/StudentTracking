using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, ILessonService lessonService, IUserService userService)
        {
            _adminService = adminService;
            _lessonService = lessonService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            UserforListPage userforListPage = new UserforListPage();

            userforListPage.Users = await _adminService.GetUserListAsync();

            return View(userforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveUser(int userId)
        {
            UserforPage userforPage = new UserforPage();
            List<UserforPage> SearchList = new List<UserforPage>
                {
                 new UserforPage { UserTypeId = 2, UserTypeName = "Öğretmen" },
                 new UserforPage { UserTypeId = 3, UserTypeName = "Veli" },
                 new UserforPage { UserTypeId = 4, UserTypeName = "Öğrenci" }
                };

            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            userforPage.User = await _adminService.GetUserAsync(userId);
            return View(userforPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserforPage userforPage)
        {
            List<UserforPage> SearchList = new List<UserforPage>
                {
                 new UserforPage { UserTypeId = 2, UserTypeName = "Öğretmen" },
                 new UserforPage { UserTypeId = 3, UserTypeName = "Veli" },
                 new UserforPage { UserTypeId = 4, UserTypeName = "Öğrenci" }
                };

            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            await _adminService.SaveorUpdateUser(userforPage.User);

            return RedirectToAction("UserList", "Admin");
        }

        [HttpPost]
        public async Task<bool> DeleteUser(int userId)
        {
            return await _adminService.DeleteUser(userId);
        }

        [HttpGet]
        public async Task<IActionResult> LessonList()
        {
            LessonforListPage lessonforListPage = new LessonforListPage();

            lessonforListPage.Lessons = await _lessonService.GetLessonListAsync(new GetLessonListRequestDto
            {
                UserId = SessionContext.GetInt("UserId"),
                UserTypeId = SessionContext.GetInt("UserTypeId"),
            });

            return View(lessonforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveLesson(int lessonId)
        {
            LessonforPage lessonforPage = new LessonforPage();

            lessonforPage.Lesson = await _lessonService.GetLessonAsync(lessonId);
            lessonforPage.TeacherList = await _userService.GetUserDataListAsync(UserTypes.Teacher);
            return View(lessonforPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveLesson(LessonforPage lessonforPage)
        {
            await _lessonService.SaveorUpdateLesson(lessonforPage.Lesson);
            lessonforPage.TeacherList = await _userService.GetUserDataListAsync(UserTypes.Teacher);

            return RedirectToAction("LessonList", "Admin");
        }

        [HttpPost]
        public async Task<bool> DeleteLesson(int lessonId)
        {
            return await _lessonService.DeleteLesson(lessonId);
        }
    }
}
