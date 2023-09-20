using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameBoringToDoList.Application.DTO;
using SameBoringToDoList.Application.ToDoLists.Commands.CreateToDoList;
using SameBoringToDoList.Application.ToDoLists.Commands.DeleteToDoList;
using SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsForUser;
using SameBoringToDoList.Application.ToDoLists.Queries.GetAllToDoListsWithItemsForUser;
using SameBoringToDoList.Application.ToDoLists.Queries.GetToDoListById;

namespace SameBoringToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo")]
    public class ToDoListController : ApiController
    {
        public ToDoListController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var command = new CreateToDoListCommand(id, request.Title);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Created(CreateResourceLocationUrl(id), null): BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ToDoListDto>> GetToDoList([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetToDoListByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteToDoList([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new DeleteToDoListCommand(id);
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDto>>> GetAllToDoLists(CancellationToken cancellationToken)
        {
            var query = new GetAllToDoListsForUserQuery();
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("item")]
        public async Task<ActionResult<IEnumerable<ToDoListWithItemsDto>>> GetAllToDoListsWithItems(CancellationToken cancellationToken)
        {
            var query = new GetAllToDoListsWithItemsForUserQuery();
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
