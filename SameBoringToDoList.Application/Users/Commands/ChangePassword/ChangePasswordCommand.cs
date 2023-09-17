using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(string CurrentPassword, string NewPassword) : ICommand;
}
