namespace SameBoringToDoList.Infrastructure.Services.Options
{
    public class SmtpOptions
    {
        public const string SectionName = "SmtpOptions";
        public string Host { get; init; }
        public int Port { get; init; }
        public string From { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
