using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        protected string GetRequestPath(object id)
        {
            return $"{Request.Path.Value}/{id}";
        }
    }
}
