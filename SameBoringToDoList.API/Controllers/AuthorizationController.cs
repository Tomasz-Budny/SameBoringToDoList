using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.Users.Commands.ChangePassword;
using SameBoringToDoList.Application.Users.Commands.ConfirmEmail;
using SameBoringToDoList.Application.Users.Commands.Register;
using SameBoringToDoList.Application.Users.Queries.Login;

namespace SameBoringToDoList.API.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : ApiController
    {
        public AuthorizationController(ISender sender) : base(sender) { }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var command = new RegisterUserCommand(id, request.Email, request.Password);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Created(CreateResourceLocationUrl(id), null) : BadRequest(result.Error);  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserQuery query, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid token, CancellationToken cancellationToken)
        {
            var command = new ConfirmEmailCommand(token);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
