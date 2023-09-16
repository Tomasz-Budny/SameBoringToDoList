using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.Users.Commands.Register
{
    public record RegisterUserCommand(Guid userId, string Login, string Password) : ICommand;
}
