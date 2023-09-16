using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoItem.Commands.AddToDoItem
{
    public class AddToDoItemCommandHandler : ICommandHandler<AddToDoItemCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public AddToDoItemCommandHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Result> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoListId = ToDoListId.Create(request.ToDoListId);
            var toDoList = await _toDoListRepository.GetAsync(toDoListId.Value, cancellationToken);

            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            var id = ToDoItemId.Create(request.itemId);

            var title = ToDoItemTitle.Create(request.Title);
            if (title.IsFailure) return title.Error;

            var description = ToDoItemDescription.Create(request.Description);
            if (description.IsFailure) return description.Error;

            var toDoItem = new Domain.Entities.ToDoItem(id.Value, title.Value, description.Value, false);

            toDoList.Add(toDoItem);

            await _toDoListRepository.UpdateAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
