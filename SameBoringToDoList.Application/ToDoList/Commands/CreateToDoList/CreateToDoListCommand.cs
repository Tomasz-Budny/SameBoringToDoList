using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList
{
    public record CreateToDoListCommand(Guid ToDoListId, string Title) : ICommand
    {
    }
}
