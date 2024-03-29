﻿using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItems.Commands.UpdateToDoItem
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

            var toDoItem = toDoList.GetItem(request.Title);
            if (toDoItem.IsFailure) return toDoItem.Error;

            var itemTitle = ToDoItemTitle.Create(request.NewTitle ?? toDoItem.Value.Title);
            if (itemTitle.IsFailure) return itemTitle.Error;

            var itemDescription = ToDoItemDescription.Create(request.NewDescription ?? toDoItem.Value.Description);
            if (itemDescription.IsFailure) return itemDescription.Error;

            toDoItem.Value.Title = itemTitle;
            toDoItem.Value.Description = itemDescription;
            toDoItem.Value.IsDone = request.IsDone ?? toDoItem.Value.IsDone;

            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
