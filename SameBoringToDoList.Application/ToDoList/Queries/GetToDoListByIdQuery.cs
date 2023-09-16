using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;

namespace SameBoringToDoList.Application.ToDoList.Queries
{
    public record GetToDoListByIdQuery(Guid id): IQuery<ToDoListDto>
    {
    }
}
