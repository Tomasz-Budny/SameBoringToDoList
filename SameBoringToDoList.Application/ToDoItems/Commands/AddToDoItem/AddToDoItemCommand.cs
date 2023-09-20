using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.ToDoItems.Commands.AddToDoItem
{
    public record AddToDoItemCommand(Guid ToDoListId, Guid ItemId, string NewToDoItemTitle, string NewToDoItemDescription) : ICommand
    {
    }
}
