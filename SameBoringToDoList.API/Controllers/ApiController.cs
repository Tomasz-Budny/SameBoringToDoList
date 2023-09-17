using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender _sender;

        protected ApiController(ISender sender)
        {
            _sender = sender;
        }

        protected string CreatedResourceLocation(object id)
        {
            return $"{Request.Path.Value}/{id}";
        }

        protected Guid GetSenderId()
        {
            var sub = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if(sub == null) return new Guid();

            var idIsvalid = Guid.TryParse(sub.Value, out var subId);
            if(idIsvalid) return subId;
            return new Guid();
        }
    }
}
