using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record AuthorId
    {
        public Guid Value { get; set; }

        private AuthorId(Guid value)
        {
            Value = value;
        }

        public static Result<AuthorId> Create(Guid value)
        {
            if(value == Guid.Empty) 
                return DomainErrors.AuthorIdIsEmpty;

            return new AuthorId(value);
        }

        public static implicit operator Guid(AuthorId authorId) => authorId.Value;

    }
}
