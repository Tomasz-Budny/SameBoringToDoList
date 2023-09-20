using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoItems.Commands.UpdateToDoItem
{
    public record UpdateToDoItemCommand(
        Guid ToDoListId, 
        string Title, 
        string? NewTitle, 
        string? NewDescription, 
        bool? IsDone
    ) : ICommand;
}
