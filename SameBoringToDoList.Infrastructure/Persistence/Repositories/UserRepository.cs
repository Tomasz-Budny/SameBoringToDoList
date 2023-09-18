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

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetWithCredentialsAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<User> GetByEmailWithCredentialsAsync(Email email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);
        }

        public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);
        }

        public async Task<User> GetByVerificationTokenAsync(Guid token, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Credential.VerificationToken == token, cancellationToken: cancellationToken);
        }

        public async Task<User> GetVerifiedWithCredentialsAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Id == id && x.Credential.VerifiedAt != null, cancellationToken: cancellationToken);
        }

        public async Task<User> GetVerifiedByEmailWithCredentialsAsync(Email email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(x => x.Credential)
                .SingleOrDefaultAsync(x => x.Email == email && x.Credential.VerifiedAt != null, cancellationToken: cancellationToken);
        }
    }
}
