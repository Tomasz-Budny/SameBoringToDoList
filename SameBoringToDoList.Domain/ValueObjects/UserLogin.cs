using SameBoringToDoList.Domain.Errors;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record UserLogin
    {
        public string Value { get; set; }
        private const int _loginMaxLength = 30;
        private const int _loginMinLength = 3;

        private UserLogin(string value)
        {
            Value = value;
        }

        public static Result<UserLogin> Create(string value)
        {
            if (value.Length > _loginMaxLength)
                return DomainErrors.UserLoginIsTooLong;
            if (value.Length < _loginMinLength)
                return DomainErrors.UserLoginIsTooShort;

            return new UserLogin(value);
        }
    }
}
