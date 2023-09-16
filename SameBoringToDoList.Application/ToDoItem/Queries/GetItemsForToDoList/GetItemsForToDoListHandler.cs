using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItem.Queries.GetItemsForToDoList
{
    public class GetItemsForToDoListHandler : IQueryHandler<GetItemsForToDoListQuery, IEnumerable<ToDoItemDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetItemsForToDoListHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Result<IEnumerable<ToDoItemDto>>> Handle(GetItemsForToDoListQuery request, CancellationToken cancellationToken)
        {
            var toDoListId = ToDoListId.Create(request.toDoListId);
            if (toDoListId.IsFailure) return toDoListId.Error;

            var toDoList = await _toDoListRepository.GetAsync(toDoListId.Value, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            var toDoItems = toDoList.ToDoItems.Select(item => item.AsDTO()).ToList();
            return toDoItems;
        }
    }
}
