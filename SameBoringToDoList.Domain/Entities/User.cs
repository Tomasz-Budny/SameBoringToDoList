using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain.Primitives;

namespace SameBoringToDoList.Domain.Entities
{
    public class User : AggregateRoot<UserId>
    {
        public Email Email { get; set; }
        public Credential Credential { get; set; }
        public User(UserId id, Email email) : base(id)
        {
            Email = email;
        }

        public User(UserId id, Email email, Credential credential) : base(id)
        {
            Email = email;
            Credential = credential;
        }
    }
}
