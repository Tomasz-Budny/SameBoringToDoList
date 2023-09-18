using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.Users.Commands.Register
{
    public record RegisterUserCommand(Guid UserId, string Email, string Password) : ICommand;
}
