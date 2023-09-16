using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoList.Queries.GetToDoListById
{
    public record GetToDoListByIdQuery(Guid id) : IQuery<ToDoListDto>
    {
    }
}
