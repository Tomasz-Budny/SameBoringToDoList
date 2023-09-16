using SameBoringToDoList.Application.Abstractions.Messaging;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Errors;
using SameBoringToDoList.Domain.Repositories;
using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.ToDoList.Queries.GetToDoListById
{
    public class GetToDoListByIdQueryHandler : IQueryHandler<GetToDoListByIdQuery, ToDoListDto>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetToDoListByIdQueryHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Result<ToDoListDto>> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken)
        {
            var toDoListId = ToDoListId.Create(request.id);

            if (toDoListId.IsFailure) return toDoListId.Error;

            var toDoList = await _toDoListRepository.GetAsync(toDoListId.Value, cancellationToken);

            if (toDoList == null) return ApplicationErrors.ToDoListNotFound;

            return toDoList.AsDTO();
        }
    }
}
