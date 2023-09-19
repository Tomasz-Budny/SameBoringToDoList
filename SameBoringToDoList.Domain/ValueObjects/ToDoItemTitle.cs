using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoItemTitle
    {
        public string Value { get; set; }
        private const int _titleMaxLength = 30;

        private ToDoItemTitle(string value)
        {
            Value = value;
        }

        public static Result<ToDoItemTitle> Create(string? value)
        {
            if (value == null) 
                return DomainErrors.NullReference;
            if (value.Length > _titleMaxLength) 
                return DomainErrors.ToDoItemTitleIsTooLong(_titleMaxLength);

            return new ToDoItemTitle(value);
        }

        public static implicit operator string(ToDoItemTitle toDoItemTitle) => toDoItemTitle.Value;
    }
}
