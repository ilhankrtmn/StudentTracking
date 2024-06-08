using Microsoft.AspNetCore.Mvc;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.App.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserService _userService;

        public StudentController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> StudentLessonList()
        {
            StudentLessonListforListPage studentLessonListforList = new StudentLessonListforListPage();

            studentLessonListforList = await _userService.GetStudentLessonListAsync(SessionContext.GetInt("UserId"));

            return View(studentLessonListforList);
        }
    }
}