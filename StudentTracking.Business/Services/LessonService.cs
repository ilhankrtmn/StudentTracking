using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models;
using System.Web.Mvc;

namespace StudentTracking.Business.Services
{
    public class LessonService : ILessonService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;

        public LessonService(IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Lesson>> GetLessonListAsync(GetLessonListRequestDto requestDto)
        {
            if (requestDto.UserTypeId == 1)
            {
                return (await _lessonRepository.GetAllAsync()).OrderByDescending(p => p.CreatedDate).ToList();
            }
            else
            {
                return (await _lessonRepository.FindListAsync(p => p.UserId == requestDto.UserId && p.Status == true)).OrderByDescending(p => p.CreatedDate).ToList();
            }
        }

        public async Task<Lesson> GetLessonAsync(int lessonId)
        {
            return await _lessonRepository.FindAsync(p => p.Id == lessonId);
        }

        public async Task SaveorUpdateLesson(Lesson lesson)
        {
            _lessonRepository.AddOrUpdate(lesson);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteLesson(int lessonId)
        {
            var lesson = await _lessonRepository.FindAsync(p => p.Id == lessonId);
            if (lesson != null)
            {
                _lessonRepository.Delete(lesson);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<List<SelectListItem>> GetTeacherListAsync()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var data = await _userRepository.FindListAsync(p => p.UserTypeId == (int)UserTypes.Teacher);

            foreach (var item in data)
            {
                items.Add(new SelectListItem { Text = item.Id.ToString(), Value = item.Name });
            }

            return items;
        }
    }
}
