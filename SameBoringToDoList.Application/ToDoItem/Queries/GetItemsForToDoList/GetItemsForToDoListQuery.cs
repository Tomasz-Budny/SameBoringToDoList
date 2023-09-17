using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoItem.Queries.GetItemsForToDoList
{
    public record GetItemsForToDoListQuery(Guid toDoListId, Guid SenderId) : IQuery<IEnumerable<ToDoItemDto>>
    {
    }
}
