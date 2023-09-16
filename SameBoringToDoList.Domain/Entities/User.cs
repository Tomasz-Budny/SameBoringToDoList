using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain.Primitives;

namespace SameBoringToDoList.Domain.Entities
{
    public class User : AggregateRoot<UserId>
    {
        public UserLogin Login { get; set; }

        public Credential Credential { get; set; }
        public User(UserId id, UserLogin login) : base(id)
        {
            Login = login;
        }

        public User(UserId id, UserLogin login, Credential credential) : base(id)
        {
            Login = login;
            Credential = credential;
        }
    }
}
