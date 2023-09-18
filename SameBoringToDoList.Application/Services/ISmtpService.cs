namespace SameBoringToDoList.Application.Services
{
    public interface ISmtpService
    {
        void SendConfirmationEmail(string email);

        void SendForgetPasswordEmail(string email);
    }
}
