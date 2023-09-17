using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoList.Queries.GetToDoListById
{
    public record GetToDoListByIdQuery(Guid Id, Guid SenderId) : IQuery<ToDoListDto>
    {
    }
}
