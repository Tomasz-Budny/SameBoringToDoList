using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoItemId
    {
        public Guid Value { get; set; }

        private ToDoItemId(Guid value)
        {
            Value = value;
        }

        public static Result<ToDoItemId> Create(Guid value)
        {
            if(value == Guid.Empty) 
                return DomainErrors.ToDoItemIdIsEmpty;

            return new ToDoItemId(value);
        }

        public static implicit operator Guid(ToDoItemId toDoItemId) => toDoItemId.Value;

    }
}
