using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain;

namespace SameBoringToDoList.Domain.Events
{
    public record ToDoItemIsDone(ToDoList list, ToDoItem item): IDomainEvent
    {
    }
}
