using MediatR;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
