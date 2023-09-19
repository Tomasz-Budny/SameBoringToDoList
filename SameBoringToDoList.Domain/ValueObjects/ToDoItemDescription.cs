using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoItemDescription
    {
        public string Value { get; set; }
        private const int _titleMaxLength = 1000;

        private ToDoItemDescription(string value)
        {
            Value = value;
        }

        public static Result<ToDoItemDescription> Create(string? value)
        {
            if (value == null)
                return DomainErrors.NullReference;

            if (value.Length > _titleMaxLength) 
                return DomainErrors.ToDoItemDescriptionIsTooLong(_titleMaxLength);

            return new ToDoItemDescription(value);
        }

        public static implicit operator string(ToDoItemDescription toDoItemTitle) => toDoItemTitle.Value;
    }
}
