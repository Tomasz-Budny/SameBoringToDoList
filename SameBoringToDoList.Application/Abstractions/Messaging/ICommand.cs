using MediatR;
using SameBoringToDoList.Shared.Errors;

namespace SameBoringToDoList.Application.Abstractions.Messaging
{
    public interface ICommand: IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
