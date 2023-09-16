using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList
{
    public class CreateToDoListCommandHandler : ICommandHandler<CreateToDoListCommand>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public CreateToDoListCommandHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<Result> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            var id = ToDoListId.Create(Guid.NewGuid());

            var title = ToDoListTitle.Create(request.title);
            if (title.IsFailure) return title.Error;

            var authorId = AuthorId.Create(request.authorId);
            if (authorId.IsFailure) return authorId.Error;

            var toDoList = new Domain.Entities.ToDoList(id.Value, title.Value, authorId.Value);
            await _toDoListRepository.AddAsync(toDoList, cancellationToken);

            return Result.Success();
        }
    }
}
