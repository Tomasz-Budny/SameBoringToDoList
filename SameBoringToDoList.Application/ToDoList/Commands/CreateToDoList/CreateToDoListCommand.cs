using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList
{
    public record CreateToDoListCommand(Guid authorId, string title, string description) : ICommand
    {
    }
}
