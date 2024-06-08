using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models.PageModel;
using System.Web.Mvc;

namespace StudentTracking.Business.Services
{
    public class UserService : IUserService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IOutgoingMailRepository _outgoingMailRepository;
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IGradeRepository _gradeRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOutgoingMailRepository outgoingMailRepository,
            IAbsenceRepository absenceRepository, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _outgoingMailRepository = outgoingMailRepository;
            _absenceRepository = absenceRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<List<SelectListItem>> GetUserDataListAsync(UserTypes userTypes)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = await _userRepository.FindListAsync(p => p.UserTypeId == (int)userTypes);

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = item.Name });
            }

            return items;
        }

        public async Task<string> GetUserMailAsync(int userId)
        {
            return (await _userRepository.FindAsync(p => p.Id == userId)).Email;
        }

        public async Task<List<OutgoingMail>> GetTeacherMailListAsync(int userId)
        {
            return (await _outgoingMailRepository.FindListAsync(p => p.SendUserId == userId)).OrderByDescending(p => p.CreatedDate).ToList();
        }

        public async Task<OutgoingMail> GetTeacherMailDetailAsync(int userId, int mailId)
        {
            return await _outgoingMailRepository.FindAsync(p => p.Id == mailId && p.SendUserId == userId);
        }

        public async Task<List<StudenttoLesson>> GetStudentListAsync(int lessonId)
        {
            return await _userRepository.GetStudentList(lessonId);
        }

        public async Task SaveStudenttoLessonAsync(StudenttoLessonforListPage requestDto)
        {
            requestDto.StudenttoLessons = requestDto.StudenttoLessons.Where(p => p.Status == true).ToList();
            foreach (var item in requestDto.StudenttoLessons)
            {
                var existingAbsence = await _absenceRepository.FindAsync(p => p.LessonId == requestDto.LessonId && p.StudentId == item.Id);
                if (existingAbsence == null)
                {
                    _absenceRepository.Add(new Absence
                    {
                        StudentId = item.Id,
                        LessonId = requestDto.LessonId,
                    });
                }

                var existingGrade = await _gradeRepository.FindAsync(p => p.LessonId == requestDto.LessonId && p.StudentId == item.Id);
                if (existingGrade == null)
                {
                    _gradeRepository.Add(new Grade
                    {
                        StudentId = item.Id,
                        LessonId = requestDto.LessonId,
                    });
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}