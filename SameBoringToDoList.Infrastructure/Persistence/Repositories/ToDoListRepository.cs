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

        public Task DeleteAsync(ToDoListId id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDoList> GetAsync(ToDoListId id, CancellationToken cancellationToken)
        {
            return await _dbContext.ToDoLists
                .Include(x => x.ToDoItems)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(ToDoList toDoList, CancellationToken cancellationToken)
        {
            _dbContext.Update(toDoList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
