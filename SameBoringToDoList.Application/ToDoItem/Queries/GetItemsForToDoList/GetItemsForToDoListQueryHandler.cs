using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItem.Queries.GetItemsForToDoList
{
    public class GetItemsForToDoListQueryHandler : IQueryHandler<GetItemsForToDoListQuery, IEnumerable<ToDoItemDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetItemsForToDoListQueryHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Result<IEnumerable<ToDoItemDto>>> Handle(GetItemsForToDoListQuery request, CancellationToken cancellationToken)
        {
            var toDoListId = ToDoListId.Create(request.toDoListId);
            if (toDoListId.IsFailure) return toDoListId.Error;

            var senderId = UserId.Create(request.SenderId);
            if (senderId.IsFailure) return senderId.Error;

            var toDoList = await _toDoListRepository.GetAsync(senderId, toDoListId, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            var toDoItems = toDoList.ToDoItems.Select(item => item.AsDTO()).ToList();
            return toDoItems;
        }
    }
}
