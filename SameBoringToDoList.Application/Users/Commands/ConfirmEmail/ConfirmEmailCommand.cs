using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.Users.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(Guid verificationToken) : ICommand;
}
