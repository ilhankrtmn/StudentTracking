using StudentTracking.Business.Interfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using StudentTracking.Business.Configuraions;

namespace StudentTracking.Business.Services
{
	public class EmailService : IEmailService, IScopedService
    {
		private readonly EmailConfiguration _emailConfiguration;
		public EmailService(EmailConfiguration emailConfiguration)
		{
			_emailConfiguration = emailConfiguration;
		}

		public void SendMail(string mailTo, string subject, string body, string from = null)
		{
			// Çağırma yöntemi = _emailService.SendMail("ilhankertmen_@hotmail.com", "DENEME", "configuration'dan çekilen data kısmı burası", "");

			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
			email.To.Add(MailboxAddress.Parse(mailTo));
			email.Subject = subject;
			email.Body = new TextPart(TextFormat.Plain) { Text = body };

			using var smtp = new SmtpClient();
			smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
			smtp.Send(email);
			smtp.Disconnect(true);
		}

	}
}
