using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record CredentialId
    {
        public Guid Value { get; set; }

        private CredentialId(Guid value)
        {
            Value = value;
        }

        public static Result<CredentialId> Create(Guid value)
        {
            if (value == Guid.Empty)
                return DomainErrors.CredentialIdIsEmpty;

            return new CredentialId(value);
        }
    }
}
