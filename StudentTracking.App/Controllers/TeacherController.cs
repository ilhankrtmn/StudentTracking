using Microsoft.AspNetCore.Mvc;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.App.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILessonService _lessonService;
        private readonly IGradeService _gradeService;
        private readonly IAbsenceService _absenceService;
        private readonly IEmailService _emailService;

        public TeacherController(IUserService userService, ILessonService lessonService, IGradeService gradeService, IAbsenceService absenceService, IEmailService emailService)
        {
            _userService = userService;
            _lessonService = lessonService;
            _gradeService = gradeService;
            _absenceService = absenceService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> LessonDetail(int lessonId)
        {
            LessonforPage lessonforPage = new LessonforPage();

            lessonforPage.Lesson = await _lessonService.GetLessonAsync(lessonId);
            lessonforPage.TeacherList = await _userService.GetUserDataListAsync(UserTypes.Teacher);

            return View(lessonforPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveGrade(int lessonId)
        {
            GradeforListPage gradeforListPage = new GradeforListPage();
            //TODO: Ekranlar kısmına öğrenci numarasını da ekle

            gradeforListPage.Grades = await _gradeService.GetGradeListAsync(new GetGradeListRequestDto
            {
                LessonId = lessonId,
                UserId = SessionContext.GetInt("UserId"),
            });

            return View(gradeforListPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGrade(GradeforListPage gradeforListPage)
        {
            await _gradeService.SaveGrade(gradeforListPage);

            gradeforListPage.Grades = await _gradeService.GetGradeListAsync(new GetGradeListRequestDto
            {
                LessonId = gradeforListPage.Grades.Select(p => p.LessonId).FirstOrDefault(),
                UserId = SessionContext.GetInt("UserId"),
            });

            return View(gradeforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> SaveAbsence(int lessonId)
        {
            AbsenceforListPage absenceforListPage = new AbsenceforListPage();
            //TODO: Ekranlar kısmına öğrenci numarasını da ekle
            absenceforListPage.Absences = await _absenceService.GetAbsenceListAsync(new GetAbsenceListRequestDto
            {
                LessonId = lessonId,
                UserId = SessionContext.GetInt("UserId"),
            });

            return View(absenceforListPage);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAbsence(AbsenceforListPage absenceforListPage)
        {
            await _absenceService.SaveAbsence(absenceforListPage);

            absenceforListPage.Absences = await _absenceService.GetAbsenceListAsync(new GetAbsenceListRequestDto
            {
                LessonId = absenceforListPage.Absences.Select(p => p.LessonId).FirstOrDefault(),
                UserId = SessionContext.GetInt("UserId"),
            });

            return View(absenceforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> MailContactList()
        {
            MailContactforListPage mailContactforListPage = new MailContactforListPage();

            mailContactforListPage.OutgoingMail = await _userService.GetTeacherMailListAsync(SessionContext.GetInt("UserId"));

            return View(mailContactforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> MailContact(int mailId)
        {
            //TODO: Burada mail detayına giderse içeriği değiştiremeyecek şekilde ayarlamalısın.

            MailContactforPage mailContactforPage = new MailContactforPage();
            // TODO Burada sadece kendi dersini alan öğrencilerin velisini görmesi gerekmektedir.
            // TODO Önce dersi seçtirip o dersin öğrencilerinin velisini görebilirse daha iyi olur.
            mailContactforPage.GuardianList = await _userService.GetUserDataListAsync(UserTypes.Guardian);
            mailContactforPage.OutgoingMail = await _userService.GetTeacherMailDetailAsync(SessionContext.GetInt("UserId"), mailId);

            return View(mailContactforPage);
        }

        [HttpPost]
        public async Task<IActionResult> MailContact(MailContactforPage mailContactforPage)
        {
            // TODO Burada sadece kendi dersini alan öğrencilerin velisini görmesi gerekmektedir.
            // TODO Önce dersi seçtirip o dersin öğrencilerinin velisini görebilirse daha iyi olur.
            mailContactforPage.GuardianList = await _userService.GetUserDataListAsync(UserTypes.Guardian);

            mailContactforPage.OutgoingMail.Email = await _userService.GetUserMailAsync(mailContactforPage.OutgoingMail.RecipientUserId);

            await _emailService.MailContact(new MailContactRequestDto
            {
                Email = mailContactforPage.OutgoingMail.Email,
                Subject = mailContactforPage.OutgoingMail.Subject,
                Message = mailContactforPage.OutgoingMail.Message,
                SendUserId = SessionContext.GetInt("UserId"),
                RecipientUserId = mailContactforPage.OutgoingMail.RecipientUserId
            });

            return View(mailContactforPage);
        }
    }
}
