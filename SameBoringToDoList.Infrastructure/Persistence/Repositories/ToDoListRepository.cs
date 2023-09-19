using Microsoft.EntityFrameworkCore;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Infrastructure.Persistence.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly SameBoringToDoListDbContext _dbContext;

        public ToDoListRepository(SameBoringToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ToDoList toDoList, CancellationToken cancellationToken)
        {
            _dbContext.Add(toDoList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ToDoList toDoList, CancellationToken cancellationToken)
        {
            _dbContext.Remove(toDoList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ToDoList> GetAsync(UserId authorId, ToDoListId id, CancellationToken cancellationToken)
        {
            return await _dbContext.ToDoLists
                .Include(x => x.ToDoItems)
                .SingleOrDefaultAsync(x => x.Id == id && x.AuthorId == authorId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<ToDoList>> GetAllListsForUserAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.ToDoLists
                .Where(x => x.AuthorId == id)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<ToDoList>> GetAllListsWithItemsForUserAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _dbContext.ToDoLists
                .Where(x => x.AuthorId == id)
                .Include(x => x.ToDoItems)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(ToDoList toDoList, CancellationToken cancellationToken)
        {
            _dbContext.Update(toDoList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
