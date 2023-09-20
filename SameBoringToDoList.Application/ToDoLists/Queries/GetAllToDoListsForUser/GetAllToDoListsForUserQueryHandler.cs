using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsForUser
{
    public class GetAllToDoListsForUserQueryHandler : IQueryHandler<GetAllToDoListsForUserQuery, IEnumerable<ToDoListDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public GetAllToDoListsForUserQueryHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }
        public async Task<Result<IEnumerable<ToDoListDto>>> Handle(GetAllToDoListsForUserQuery request, CancellationToken cancellationToken)
        {
            var senderId = UserId.Create(_userContext.GetUserId);
            if (senderId.IsFailure) return senderId.Error;

            var toDoLists = await _toDoListRepository.GetAllListsForUserAsync(senderId, cancellationToken);
            if (toDoLists == null) return ApplicationErrors.ToDoListNotFound;

            return toDoLists.Select(x => x.AsDto()).ToList();
        }
    }
}
