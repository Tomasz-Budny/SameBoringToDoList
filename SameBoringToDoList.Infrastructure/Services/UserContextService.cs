using Microsoft.AspNetCore.Http;
using SameBoringToDoList.Application.Services;
using System.Security.Claims;

namespace SameBoringToDoList.Infrastructure.Services
{
    public class UserContextService : IUserContextService<Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public Guid GetUserId
        {
            get
            {
                var sub = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (sub == null) return new Guid();
                var idIsvalid = Guid.TryParse(sub.Value, out var subId);
                if (idIsvalid) return subId;
                return new Guid();
            }
        }
    }
}
