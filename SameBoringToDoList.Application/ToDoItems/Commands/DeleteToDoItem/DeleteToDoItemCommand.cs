using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoItems.Commands.DeleteToDoItem
{
    public record DeleteToDoItemCommand(Guid ToDoListId, string Title) : ICommand;
}
