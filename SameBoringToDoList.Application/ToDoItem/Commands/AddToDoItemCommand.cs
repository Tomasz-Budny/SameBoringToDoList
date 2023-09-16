using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoItem.Commands
{
    public record AddToDoItemCommand(Guid ToDoListId, Guid itemId,  string Title, string Description): ICommand
    {
    }
}
