using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record UserId
    {
        public Guid Value { get; set; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static Result<UserId> Create(Guid value)
        {
            if (value == Guid.Empty)
                return DomainErrors.UserIdIsEmpty;

            return new UserId(value);
        }

        public static implicit operator Guid(UserId userId) => userId.Value;
    }
}
