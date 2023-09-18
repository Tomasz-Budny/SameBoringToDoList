using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Infrastructure.Services.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Infrastructure.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly SmtpOptions _smtpOptions;

        public SmtpService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }
        public void SendConfirmationEmail(Email destinationEmail, Guid verificationToken)
        {
            var body = $"<h1>Verify Email</h1><br><p>Your confirmation token: <b>{verificationToken}</b></p>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpOptions.From));
            email.To.Add(MailboxAddress.Parse(destinationEmail.Value));
            email.Subject = "Verify email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpOptions.UserName, _smtpOptions.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendForgetPasswordEmail(Email email, Guid verificationToken)
        {
            throw new NotImplementedException();
        }
    }
}
