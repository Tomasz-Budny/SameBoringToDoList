using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoList.Commands.DeleteToDoList
{
    public record DeleteToDoListCommand(Guid ToDoListId) : ICommand
    {
    }
}
