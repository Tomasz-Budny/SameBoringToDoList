using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;
using System.Linq;

namespace SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsWithItemsForUser
{
    internal class GetAllToDoListsWithItemsForUserQueryHandler : IQueryHandler<GetAllToDoListsWithItemsForUserQuery, IEnumerable<ToDoListWithItemsDto>>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public GetAllToDoListsWithItemsForUserQueryHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }

        public async Task<Result<IEnumerable<ToDoListWithItemsDto>>> Handle(GetAllToDoListsWithItemsForUserQuery request, CancellationToken cancellationToken)
        {
            var authorId = UserId.Create(_userContext.GetUserId);
            if (authorId.IsFailure) return authorId.Error;

            var toDoLists = await _toDoListRepository.GetAllListsWithItemsForUserAsync(authorId, cancellationToken);
            if (toDoLists == null) return ApplicationErrors.ToDoListNotFound;

            return Result.Success(toDoLists.Select(x => x.AsDtoWithItems()));
        }
    }
}
