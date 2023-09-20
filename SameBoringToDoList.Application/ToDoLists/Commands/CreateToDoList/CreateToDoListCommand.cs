using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoLists.Commands.CreateToDoList
{
    public record CreateToDoListCommand(Guid ToDoListId, string Title) : ICommand
    {
    }
}
