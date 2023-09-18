using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Application.Services
{
    public interface ISmtpService
    {
        void SendConfirmationEmail(Email email, Guid verificationToken);

        void SendForgetPasswordEmail(Email email, Guid verificationToken);
    }
}
