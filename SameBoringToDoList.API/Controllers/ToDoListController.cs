using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoList.Commands.CreateToDoList;
using SameBoringToDoList.Application.ToDoList.Commands.DeleteToDoList;
using SameBoringToDoList.Application.ToDoList.Queries.GetAllToDoListsForUser;
using SameBoringToDoList.Application.ToDoList.Queries.GetAllToDoListsWithItemsForUser;
using SameBoringToDoList.Application.ToDoList.Queries.GetToDoListById;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo")]
    public class ToDoListController : ApiController
    {
        public ToDoListController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListRequest request)
        {
            var id = Guid.NewGuid();
            var command = new CreateToDoListCommand(id, request.Title);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Created(CreateResourceLocationUrl(id), null): BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetToDoList([FromRoute] Guid id)
        {
            var query = new GetToDoListByIdQuery(id);
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteToDoList([FromRoute] Guid id)
        {
            var query = new DeleteToDoListCommand(id);
            var result = await _sender.Send(query);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDoLists()
        {
            var query = new GetAllToDoListsForUserQuery();
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetAllToDoListsWithItems()
        {
            var query = new GetAllToDoListsWithItemsForUserQuery();
            var result = await _sender.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
