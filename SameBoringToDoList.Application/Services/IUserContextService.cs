using System.Security.Claims;

namespace SameBoringToDoList.Application.Services
{
    public interface IUserContextService<TId>
    {
        ClaimsPrincipal User { get; }
        TId GetUserId { get; }
    }
}
