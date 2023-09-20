using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsForUser
{
    public record GetAllToDoListsForUserQuery() : IQuery<IEnumerable<ToDoListDto>>
    {
    }
}
