using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItem.Commands.UpdateToDoItem
{
    public class UpdateToDoItemCommandHandler : ICommandHandler<UpdateToDoItemCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public UpdateToDoItemCommandHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }
        public async Task<Result> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(_userContext.GetUserId);
            if (userId.IsFailure) return userId.Error;

            var toDoListId = ToDoListId.Create(request.ToDoListId);
            if (toDoListId.IsFailure) return toDoListId.Error;

            var toDoList = await _toDoListRepository.GetAsync(userId, toDoListId, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            var toDoItem = toDoList.ToDoItems.FirstOrDefault(x => x.Title == request.Title);
            if (toDoItem == null) return ApplicationErrors.ToDoItemNotFound;

            var itemTitle = ToDoItemTitle.Create(request.NewTitle);
            if (itemTitle.IsFailure) itemTitle = toDoItem.Title;

            var itemDescription = ToDoItemDescription.Create(request.NewDescription);
            if (itemDescription.IsFailure) itemDescription = toDoItem.Description;

            var isDone = request.IsDone != null ? (bool)request.IsDone : toDoItem.IsDone;

            toDoItem.Title = itemTitle;
            toDoItem.Description = itemDescription;
            toDoItem.IsDone = isDone;

            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
