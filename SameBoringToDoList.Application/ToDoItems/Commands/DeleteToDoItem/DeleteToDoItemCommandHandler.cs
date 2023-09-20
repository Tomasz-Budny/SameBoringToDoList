using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItems.Commands.DeleteToDoItem
{
    public class DeleteToDoItemCommandHandler : ICommandHandler<DeleteToDoItemCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public DeleteToDoItemCommandHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }

        public async Task<Result> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(_userContext.GetUserId);
            if (userId.IsFailure) return userId.Error;

            var toDoListId = ToDoListId.Create(request.ToDoListId);
            if (userId.IsFailure) return toDoListId.Error;

            var toDoList = await _toDoListRepository.GetAsync(userId, toDoListId, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            //var item = toDoList.ToDoItems.FirstOrDefault(x => x.Title == request.Title);
            var toDoItem = toDoList.GetItem(request.Title);
            if (toDoItem.IsFailure) return toDoItem.Error;

            toDoList.Remove(toDoItem);
            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
