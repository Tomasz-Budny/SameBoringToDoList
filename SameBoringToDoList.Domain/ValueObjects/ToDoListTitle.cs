using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record ToDoListTitle
    {
        public string Value { get; set; }
        private const int _titleMaxLength = 30;

        private ToDoListTitle(string value)
        {
            Value = value;
        }

        public static Result<ToDoListTitle> Create(string value)
        {
            if(value.Length > _titleMaxLength) 
                return DomainErrors.ToDoListTitleIsTooLong(_titleMaxLength);
            
            return new ToDoListTitle(value);
        }

        public static implicit operator string(ToDoListTitle toDoListTitle) => toDoListTitle.Value;
    }
}
