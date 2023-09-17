using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItem.Commands.AddToDoItem
{
    public class AddToDoItemCommandHandler : ICommandHandler<AddToDoItemCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public AddToDoItemCommandHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }
        public async Task<Result> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
        {
            var authorId = UserId.Create(_userContext.GetUserId);
            if (authorId.IsFailure) return authorId.Error;

            var toDoListId = ToDoListId.Create(request.ToDoListId);
            var toDoList = await _toDoListRepository.GetAsync(authorId, toDoListId, cancellationToken);
            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            var id = ToDoItemId.Create(request.itemId);
            if (id.IsFailure) return id.Error;

            var title = ToDoItemTitle.Create(request.Title);
            if (title.IsFailure) return title.Error;

            var description = ToDoItemDescription.Create(request.Description);
            if (description.IsFailure) return description.Error;

            var toDoItem = new Domain.Entities.ToDoItem(id, title, description, false);

            var addResult = toDoList.Add(toDoItem);
            if (addResult.IsFailure) return addResult.Error;

            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
