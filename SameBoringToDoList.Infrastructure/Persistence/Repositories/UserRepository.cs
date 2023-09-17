using Microsoft.EntityFrameworkCore;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SameBoringToDoListDbContext _dbContext;

        public UserRepository(SameBoringToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(UserId id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetWithCredentialsAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<User> GetByLoginWithCredentialsAsync(UserLogin login, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Login == login, cancellationToken: cancellationToken);
        }

        public async Task<User> GetByLoginAsync(UserLogin login, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Login == login, cancellationToken: cancellationToken);
        }
    }
}
