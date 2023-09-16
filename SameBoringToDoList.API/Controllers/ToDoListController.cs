using MediatR;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList;
using SameBoringToDoList.Application.ToDoList.Queries;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoListController : BaseController
    {
        public ToDoListController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? Created(string.Empty, result): BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetToDoList([FromRoute] GetToDoListByIdQuery command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
