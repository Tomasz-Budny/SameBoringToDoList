using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Infrastructure.Services.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;

namespace SameBoringToDoList.Infrastructure.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly SmtpOptions _smtpOptions;

        public SmtpService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }
        public void SendConfirmationEmail(string destinationEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpOptions.From));
            email.To.Add(MailboxAddress.Parse(destinationEmail));
            email.Subject = "Sample subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Test</h1>" };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpOptions.UserName, _smtpOptions.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendForgetPasswordEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
