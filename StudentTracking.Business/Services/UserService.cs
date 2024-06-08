using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;
using StudentTracking.Data.Enums;
using System.Web.Mvc;

namespace StudentTracking.Business.Services
{
    public class UserService : IUserService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IOutgoingMailRepository _outgoingMailRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOutgoingMailRepository outgoingMailRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _outgoingMailRepository = outgoingMailRepository;
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
    }
}