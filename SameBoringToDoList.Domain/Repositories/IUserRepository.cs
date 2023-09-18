using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(UserId id, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(UserId id, CancellationToken cancellationToken);
        Task<User> GetWithCredentialsAsync(UserId id, CancellationToken cancellationToken);
        Task<User> GetByEmailWithCredentialsAsync(Email email, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken);
        Task<User> GetByVerificationTokenAsync(Guid token, CancellationToken cancellationToken);
        Task<User> GetVerifiedWithCredentialsAsync(UserId id, CancellationToken cancellationToken);
        Task<User> GetVerifiedByEmailWithCredentialsAsync(Email email, CancellationToken cancellationToken);
    }
}
