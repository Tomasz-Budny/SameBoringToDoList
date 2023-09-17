using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList
{
    public record CreateToDoListCommand(Guid AuthorId, Guid ToDoListId, string Title) : ICommand
    {
    }
}
