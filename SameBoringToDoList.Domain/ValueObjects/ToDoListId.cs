using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoListId
    {
        public Guid Value { get; }

        private ToDoListId(Guid value)
        {
            Value = value;
        }

        public static Result<ToDoListId> Create(Guid value)
        {
            if(value == Guid.Empty) 
                return DomainErrors.ToDoListIdIsEmpty;

            return new ToDoListId(value);
        }

        public static implicit operator Guid(ToDoListId toDoListId) => toDoListId.Value;
    }
}
