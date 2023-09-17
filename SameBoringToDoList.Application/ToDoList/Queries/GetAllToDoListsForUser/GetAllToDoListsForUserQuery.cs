using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoList.Queries.GetAllToDoListsForUser
{
    public record GetAllToDoListsForUserQuery(Guid SenderId) : IQuery<IEnumerable<ToDoListDto>>
    {
    }
}
