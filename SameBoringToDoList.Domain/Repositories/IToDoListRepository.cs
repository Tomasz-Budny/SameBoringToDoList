using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.ValueObjects;

namespace SameBoringToDoList.Domain.Repositories
{
    public interface IToDoListRepository
    {
        Task<ToDoList> GetAsync(ToDoListId id, CancellationToken cancellationToken);
        Task<IEnumerable<ToDoList>> GetAllListsForUserAsync(UserId id, CancellationToken cancellationToken);
        Task AddAsync(ToDoList toDoList, CancellationToken cancellationToken);
        Task UpdateAsync(ToDoList toDoList, CancellationToken cancellationToken);
        Task DeleteAsync(ToDoListId id, CancellationToken cancellationToken);
    }
}
