using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoItem.Commands.DeleteToDoItem
{
    public record DeleteToDoItemCommand(Guid ToDoListId, string Title) : ICommand;
}
