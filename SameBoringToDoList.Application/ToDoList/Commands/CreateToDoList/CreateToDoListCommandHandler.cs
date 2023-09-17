using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.Services;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList
{
    public class CreateToDoListCommandHandler : ICommandHandler<CreateToDoListCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IUserContextService<Guid> _userContext;

        public CreateToDoListCommandHandler(IToDoListRepository toDoListRepository, IUserContextService<Guid> userContextService)
        {
            _toDoListRepository = toDoListRepository;
            _userContext = userContextService;
        }

        public async Task<Result> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            var id = ToDoListId.Create(request.ToDoListId);

            var title = ToDoListTitle.Create(request.Title);
            if (title.IsFailure) return title.Error;

            var authorId = UserId.Create(_userContext.GetUserId);
            if (authorId.IsFailure) return authorId.Error;

            var toDoList = new Domain.Entities.ToDoList(id, title, authorId);
            await _toDoListRepository.AddAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
