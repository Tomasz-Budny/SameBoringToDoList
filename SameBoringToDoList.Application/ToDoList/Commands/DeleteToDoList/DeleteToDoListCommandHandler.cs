using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoList.Commands.DeleteToDoList
{
    public class DeleteToDoListCommandHandler : ICommandHandler<DeleteToDoListCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public DeleteToDoListCommandHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }
        public async Task<Result> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoListId = ToDoListId.Create(request.ToDoListId);
            if (toDoListId.IsFailure) return toDoListId.Error;

            var userId = UserId.Create(_userContext.GetUserId);
            if (userId.IsFailure) return userId.Error;

            var toDoList = await _toDoListRepository.GetAsync(userId, toDoListId, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            await _toDoListRepository.DeleteAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
