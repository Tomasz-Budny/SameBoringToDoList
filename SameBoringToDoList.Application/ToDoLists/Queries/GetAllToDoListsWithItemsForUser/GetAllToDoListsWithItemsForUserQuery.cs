using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsWithItemsForUser
{
    public record GetAllToDoListsWithItemsForUserQuery() : IQuery<IEnumerable<ToDoListWithItemsDto>>;
}
