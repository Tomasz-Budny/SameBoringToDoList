using MediatR;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
