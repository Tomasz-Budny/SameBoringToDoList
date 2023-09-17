using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoList.Queries.GetAllToDoListsForUser
{
    public class GetAllToDoListsForUserQueryHandler : IQueryHandler<GetAllToDoListsForUserQuery, IEnumerable<ToDoListDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetAllToDoListsForUserQueryHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Result<IEnumerable<ToDoListDto>>> Handle(GetAllToDoListsForUserQuery request, CancellationToken cancellationToken)
        {
            var senderId = UserId.Create(request.SenderId);
            if (senderId.IsFailure) return senderId.Error;

            var toDoLists = await _toDoListRepository.GetAllListsForUserAsync(senderId, cancellationToken);
            if (toDoLists == null) return ApplicationErrors.ToDoListNotFound;

            return toDoLists.Select(x => x.AsDTO()).ToList();
        }
    }
}
