using StudentTracking.Business.Interfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using StudentTracking.Business.Configuraions;
using StudentTracking.Core;
using StudentTracking.Data.Models;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Core.Session;
using StudentTracking.Data.EntityFramework.UnitOfWork;

namespace StudentTracking.Business.Services
{
    public class EmailService : IEmailService, IScopedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IOutgoingMailRepository _outgoingMailRepository;

        public EmailService(IUnitOfWork unitOfWork, EmailConfiguration emailConfiguration, IOutgoingMailRepository outgoingMailRepository)
        {
            _unitOfWork = unitOfWork;
            _emailConfiguration = emailConfiguration;
            _outgoingMailRepository = outgoingMailRepository;
        }

        public void SendMail(SendMailRequestDto requestDto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            email.To.Add(MailboxAddress.Parse(requestDto.MailTo));
            email.Subject = requestDto.Subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = requestDto.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task MailContact(MailContactRequestDto requestDto)
        {
            var response = await _outgoingMailRepository.SendOtpMailAsync(new MailContactRequestDto
            {
                SendUserId = requestDto.SendUserId,
                RecipientUserId = requestDto.RecipientUserId,
                Email = requestDto.Email,
                Subject = requestDto.Subject,
                Message = requestDto.Message
            });

            if (response)
            {
                await _unitOfWork.CompleteAsync();

                SendMail(new SendMailRequestDto
                {
                    MailTo = requestDto.Email,
                    Subject = requestDto.Subject,
                    Body = requestDto.Message
                });
            }
            else
            {
                return;
            }
        }
    }
}