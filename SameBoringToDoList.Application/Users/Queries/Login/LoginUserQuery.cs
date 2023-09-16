using SameBoringToDoList.Application.Abstractions.Messaging;

namespace SameBoringToDoList.Application.Users.Queries.Login
{
    public record LoginUserQuery(string login, string password) : IQuery<string>;
}
