using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Core.Enums;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.App.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;

        public AdminController(IAdminService adminService, ILessonService lessonService, IUserService userService, IReportService reportService)
        {
            _adminService = adminService;
            _lessonService = lessonService;
            _userService = userService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            UserforListPage userforListPage = new UserforListPage();

            userforListPage.Users = await _adminService.GetUserListAsync();

            return View(userforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveUser(int userId, bool status)
        {
            UserforPage userforPage = new UserforPage();
            List<UserforPage> SearchList = new List<UserforPage>
                {
                 new UserforPage { UserTypeId = 2, UserTypeName = "Öğretmen" },
                 new UserforPage { UserTypeId = 3, UserTypeName = "Veli" },
                 new UserforPage { UserTypeId = 4, UserTypeName = "Öğrenci" }
                };
            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            if (status)
            {
                userforPage.StudenList = await _userService.GetStudentDataListAsync(UserTypes.Student);
            }
            else
            {
                userforPage.StudenList = await _userService.GetStudentSaveDataListAsync(UserTypes.Student);
            }

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

            userforPage.StudenList = await _userService.GetStudentDataListAsync(UserTypes.Student);

            ViewBag.SearchList = new SelectList(SearchList, "UserTypeId", "UserTypeName");

            await _adminService.SaveorUpdateUser(userforPage.User);
            switch (SessionContext.GetInt("UserTypeId"))
            {
                case (int)UserTypes.Admin:
                    return RedirectToAction("UserList", "Admin");
                case (int)UserTypes.Teacher:
                case (int)UserTypes.Student:
                    return RedirectToAction("SaveUser", "Admin", new { userId = SessionContext.GetInt("UserId") });
                default:
                    return RedirectToAction("UserList", "Admin");
            }
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

        [HttpGet]
        public async Task<IActionResult> SaveStudenttoLesson(int lessonId)
        {
            StudenttoLessonforListPage studenttoLessonforListPage = new StudenttoLessonforListPage();

            studenttoLessonforListPage.StudenttoLessons = await _userService.GetStudentListAsync(lessonId);
            studenttoLessonforListPage.LessonId = lessonId;
            return View(studenttoLessonforListPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStudenttoLesson(StudenttoLessonforListPage studenttoLessonforListPage)
        {
            await _userService.SaveStudenttoLessonAsync(studenttoLessonforListPage);

            return RedirectToAction("LessonList", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Report()
        {
            ReportforPage reportforPage = new ReportforPage();

            #region DropDownPart
            List<ReportforPage> reportTypeList = new List<ReportforPage>
            {
                new ReportforPage { ReportTypeId = 1, ReportTypeName = "Not" },
                new ReportforPage { ReportTypeId = 2, ReportTypeName = "Devamsızlık" }
            };
            ViewBag.ReportTypeList = new SelectList(reportTypeList, "ReportTypeId", "ReportTypeName");

            List<ReportforPage> situationTypeList = new List<ReportforPage>
            {
                new ReportforPage { SituationTypeId = 1, SituationTypeName = "Kalanlar" },
                new ReportforPage { SituationTypeId = 2, SituationTypeName = "Geçenler" },
                new ReportforPage { SituationTypeId = 3, SituationTypeName = "Özel Koşul" }
            };
            ViewBag.SituationTypeList = new SelectList(situationTypeList, "SituationTypeId", "SituationTypeName");

            List<ReportforPage> statusTypeList = new List<ReportforPage>
            {
                new ReportforPage { StatusTypeId = 1, StatusTypeName = "Büyük" },
                new ReportforPage { StatusTypeId = 2, StatusTypeName = "Küçük" }
            };
            ViewBag.StatusTypeList = new SelectList(statusTypeList, "StatusTypeId", "StatusTypeName");

            reportforPage.LessonList = await _lessonService.GetLessonDataSelectListAsync(new GetLessonListRequestDto
            {
                UserId = SessionContext.GetInt("UserId"),
                UserTypeId = SessionContext.GetInt("UserTypeId"),
            });
            #endregion

            return View(reportforPage);
        }

        [HttpPost]
        public async Task<IActionResult> Report(ReportforPage reportforPage)
        {
            #region DropDownPart
            List<ReportforPage> reportTypeList = new List<ReportforPage>
            {
                new ReportforPage { ReportTypeId = 1, ReportTypeName = "Not" },
                new ReportforPage { ReportTypeId = 2, ReportTypeName = "Devamsızlık" }
            };
            ViewBag.ReportTypeList = new SelectList(reportTypeList, "ReportTypeId", "ReportTypeName");

            List<ReportforPage> situationTypeList = new List<ReportforPage>
            {
                new ReportforPage { SituationTypeId = 1, SituationTypeName = "Kalanlar" },
                new ReportforPage { SituationTypeId = 2, SituationTypeName = "Geçenler" },
                new ReportforPage { SituationTypeId = 3, SituationTypeName = "Özel Koşul" }
            };
            ViewBag.SituationTypeList = new SelectList(situationTypeList, "SituationTypeId", "SituationTypeName");

            List<ReportforPage> statusTypeList = new List<ReportforPage>
            {
                new ReportforPage { StatusTypeId = 1, StatusTypeName = "Büyük" },
                new ReportforPage { StatusTypeId = 2, StatusTypeName = "Küçük" }
            };
            ViewBag.StatusTypeList = new SelectList(statusTypeList, "StatusTypeId", "StatusTypeName");

            reportforPage.LessonList = await _lessonService.GetLessonDataSelectListAsync(new GetLessonListRequestDto
            {
                UserId = SessionContext.GetInt("UserId"),
                UserTypeId = SessionContext.GetInt("UserTypeId"),
            });
            #endregion

            reportforPage = await _reportService.ReportDataAsync(reportforPage);

            ViewBag.GradesDisplayTable = reportforPage.Grades.Count > 0;
            ViewBag.AbsencesDisplayTable = reportforPage.Absences.Count > 0;

            return View(reportforPage);
        }
    }
}
