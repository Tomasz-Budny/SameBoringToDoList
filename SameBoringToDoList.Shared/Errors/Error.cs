namespace SameBoringToDoList.Shared.Errors
{
    public record Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }

        public static implicit operator string(Error error) => error?.Code ?? string.Empty;

        public static readonly Error None = new Error(string.Empty, string.Empty);
    }
}
