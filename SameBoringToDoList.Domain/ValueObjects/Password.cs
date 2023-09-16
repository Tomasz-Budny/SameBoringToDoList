using Microsoft.Identity.Client;
using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record Password
    {
        public string Value { get; set; }
        private const int _passwordMinLength = 5;


        private Password(string value)
        {
            Value = value;
        }

        public static Result<Password> Create(string value)
        {
            if (value.Length < _passwordMinLength)
                return DomainErrors.PasswordIsTooShort(_passwordMinLength);

            return new Password(value);
        }
    }
}
