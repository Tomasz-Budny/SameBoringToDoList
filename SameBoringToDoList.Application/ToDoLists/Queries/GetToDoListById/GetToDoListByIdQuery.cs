using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoLists.Queries.GetToDoListById
{
    public record GetToDoListByIdQuery(Guid Id) : IQuery<ToDoListDto>
    {
    }
}
