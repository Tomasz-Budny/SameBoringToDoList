using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoLists.Commands.DeleteToDoList
{
    public record DeleteToDoListCommand(Guid ToDoListId) : ICommand
    {
    }
}
