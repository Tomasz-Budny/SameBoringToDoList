using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItems.Commands.AddToDoItem
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

            var toDoItemTitle = ToDoItemTitle.Create(request.NewToDoItemTitle);
            if (toDoItemTitle.IsFailure) return toDoItemTitle.Error;

            var toDoItemDescription = ToDoItemDescription.Create(request.NewToDoItemDescription);
            if (toDoItemDescription.IsFailure) return toDoItemDescription.Error;

            var toDoItem = new ToDoItem(toDoItemTitle, toDoItemDescription, false);

            var addResult = toDoList.Add(toDoItem);
            if (addResult.IsFailure) return addResult.Error;

            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
