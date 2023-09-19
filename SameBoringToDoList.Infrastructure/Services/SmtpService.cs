using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Infrastructure.Services.Options;
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
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpOptions.From));
            email.To.Add(MailboxAddress.Parse(destinationEmail.Value));
            email.Subject = "Verify email";
            email.Body = new TextPart(TextFormat.Html) { Text = confirmationTemplate("Tomo", verificationToken) };

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

        private Func<string, Guid, string> confirmationTemplate = (string name, Guid token) =>
        {
            return $"  <div class=\"container\" \r\n    style=\"\r\n      box-sizing: border-box;\r\n      width: 50%; \r\n      height: 100%; \r\n      margin: auto;\r\n      height: 100%;\r\n      margin-top: 20px;\r\n      font-family: Verdana, sans-serif;\">\r\n    <p\r\n      style=\"\r\n      box-sizing: border-box;\r\n      font-size: 20px;\r\n      font-weight: 800;\r\n      margin: 0px;\">\r\n      Cześć, {name}\r\n    </p>\r\n    <p\r\n      style=\"\r\n        box-sizing: border-box;\">\r\n      Dziękujemy za rejestracje w serwisie. Aby aktywować konto wejdź w link poniżej.\r\n    </p>\r\n    <div style=\"\r\n      display: flex;\r\n      justify-content: center;\">\r\n      <a \r\n      href=\"https://localhost:4200/confirm?token={token}\" \r\n      style=\"\r\n        box-sizing: border-box;\r\n        background-color: yellow;\r\n        text-decoration: none;\r\n        color: black;\r\n        padding: 12px;\r\n        border-radius: 5px;\r\n        font-family: Verdana, sans-serif;\r\n      \">\r\n      Aktywuj konto\r\n    </a>\r\n    </div>\r\n    <div class=\"greetings-container\" \r\n      style=\"\r\n        margin-top: 20px;\r\n        display: flex;\r\n        justify-content: end;\">\r\n      <img\r\n        style=\"width: 100px;\" \r\n        src=\"https://dm0qx8t0i9gc9.cloudfront.net/watermarks/image/rDtN98Qoishumwih/happy-winking-eye-cartoon-face_XkvCxz_SB_PM.jpg\" alt=\"\">\r\n      <div style=\"\r\n        margin-top: 20px;\r\n        text-align: end;\">\r\n        Pozdrawiamy, Zespół XYZ!\r\n      </div>\r\n    </div>\r\n  </div>";
        };
    }
}
