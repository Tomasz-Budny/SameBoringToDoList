using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoItem.Queries
{
    public record GetItemsForToDoListQuery(Guid toDoListId) : IQuery<IEnumerable<ToDoItemDto>>
    {
    }
}
