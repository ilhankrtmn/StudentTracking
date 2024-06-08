using Microsoft.EntityFrameworkCore;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.Models;
using StudentTracking.Data.Models.PageModel;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class OutgoingMailRepository(StudentTrackingContext context) : EfCoreRepositoryBase<OutgoingMail>(context), IOutgoingMailRepository, IScopedRepository
    {
        private readonly StudentTrackingContext _context = context;

        public async Task InsertUserEmailOtpAsync(int UserId, int pincode)
        {
            await _context.UserEmailOtps.AddAsync(new UserEmailOtp
            {
                UserId = UserId,
                Pincode = pincode
            });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SendOtpMailAsync(MailContactRequestDto requestDto)
        {
            var dateThreshold = DateTime.Now.AddMinutes(-60);

            var outgoingId = await _context.OutgoingMails
                .Where(p => p.RecipientUserId == requestDto.RecipientUserId
                    && p.Email == requestDto.Email
                    && p.Message == requestDto.Message
                    && p.CreatedDate >= dateThreshold)
                .Select(p => (long?)p.Id)
                .SingleOrDefaultAsync();

            var outgoingMail = outgoingId ?? 0L;

            if (outgoingMail == 0)
            {
                await _context.OutgoingMails.AddAsync(new OutgoingMail
                {
                    SendUserId = requestDto.SendUserId,
                    RecipientUserId = requestDto.RecipientUserId,
                    Email = requestDto.Email,
                    Subject = requestDto.Subject,
                    Message = requestDto.Message,
                    Status = 0
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> CheckUserEmailIdAsync(int userId, int pincode)
        {
            var userEmailOtp = await _context.UserEmailOtps.Where(p => p.UserId == userId && p.Pincode == pincode && p.CreatedDate > DateTime.Now.AddMinutes(-2))
                                                        .OrderByDescending(p => p.Id)
                                                        .FirstOrDefaultAsync();

            return userEmailOtp != null ? userEmailOtp.Id : 0;
        }

        public async Task UpdateUserEmailOtpAsync(int emailId)
        {
            var userEmailOtp = await _context.UserEmailOtps.Where(p => p.Id == emailId).SingleOrDefaultAsync();
            userEmailOtp.Status = 1;
            userEmailOtp.UpdatedDate = DateTime.Now;
            _context.UserEmailOtps.Update(userEmailOtp);

            _context.SaveChanges();
        }

        public async Task<MailListforListPage> GetGuardianMailListAsync(int guardianId)
        {
            var data = await _context.OutgoingMails.Where(p => p.RecipientUserId == guardianId)
                .Select(p => new MailList
                {
                    Id = p.Id,
                    TeacherName = p.User.Name,
                    TeacherSurname = p.User.Surname,
                    Subject = p.Subject,
                    CreatedDate = p.CreatedDate
                }).ToListAsync();

            return new MailListforListPage
            {
                MailLists = data
            };
        }
    }
}